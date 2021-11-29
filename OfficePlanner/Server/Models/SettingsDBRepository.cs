using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficePlanner.Server.Data;
using OfficePlanner.Shared;
using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OfficePlanner.Server.Models
{
    public class SettingsDBRepository : ISettingsRepository
    {
        private ApplicationDbContext _context { get; set; }

        public SettingsDBRepository(ApplicationDbContext applicationDbContext)
        {
            this._context = applicationDbContext;
        }

        private static SettingReadViewModel ConvertSettingToReadModel(Setting setting)
        {
            dynamic activeSetting = JsonConvert.DeserializeObject(setting.Settings);

            SettingReadViewModel settingReadViewModel = new SettingReadViewModel
            {
                DaysRequiredInOffice = activeSetting.DaysRequiredInOffice,
                Holidays = activeSetting.Holidays.ToObject<DateTime[]>(),
                Workhours = activeSetting.Workhours.ToObject<Workhours>(),
                FutureReservationWindow = activeSetting.FutureReservationWindow,
                WeekendsAllowed = activeSetting.WeekendsAllowed,
                Id = setting.Id,
                FromDate = setting.FromDate,
                UntilDate = setting.UntilDate
            };

            return settingReadViewModel;
        }

        private static string ConvertPropertiesToJsonString(SettingCreateViewModel properties)
        {
            Properties currentProperties = new Properties
            {
                DaysRequiredInOffice = properties.DaysRequiredInOffice,
                Holidays = properties.Holidays,
                Workhours = properties.Workhours,
                FutureReservationWindow = properties.FutureReservationWindow,
                WeekendsAllowed = properties.WeekendsAllowed
            };

            return JsonConvert.SerializeObject(currentProperties);
        }

        private void UpdateDBRecord(Setting setting)
        {
            _context.Entry(setting).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private void DeleteDBRecord(Setting setting)
        {
            _context.Entry(setting).State = EntityState.Deleted;
            _context.SaveChanges();
        }

        public bool CreateSetting(SettingCreateViewModel setting)
        {
            if (!IsValidSetting(setting))
            {
                return false;
            }

            // Set UntilDate of currently active setting version to FromDate of new one -1d for versioning if previous version exists
            Setting oldActiveSetting = _context.Setting.FirstOrDefault(s => s.UntilDate == DateTime.MaxValue);
            if (oldActiveSetting != null)
            {
                oldActiveSetting.UntilDate = setting.FromDate.AddDays(-1);
                if (oldActiveSetting.UntilDate > setting.FromDate)
                {
                    return false;
                }
                _context.Entry(oldActiveSetting).State = EntityState.Modified;
            }

            Setting newSetting = new Setting
            {
                FromDate = setting.FromDate,
                UntilDate = DateTime.MaxValue,
                Settings = ConvertPropertiesToJsonString(setting)
            };
            _context.Setting.Add(newSetting);
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public SettingReadViewModel GetActiveProperties(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }
            var currentSetting = _context.Setting.FirstOrDefault(s => s.FromDate.Date <= date.Date && s.UntilDate.Date >= date.Date);
            if (currentSetting != null)
            {
                var settingReadViewModel = ConvertSettingToReadModel(currentSetting);
                return settingReadViewModel;
            }
            else
            {
                return null;
            }

        }

        public bool SettingExists(int id)
        {
            return _context.Setting.Any(e => e.Id == id);
        }

        public bool UpdateSetting(SettingUpdateViewModel setting, int id)
        {
            if (!IsValidSetting(setting))
            {
                return false;
            }
            if (SettingExists(id))
            {
                var settingObject = _context.Setting.FirstOrDefault(e => e.Id == id);
                if (settingObject.FromDate <= DateTime.Now)
                {
                    return false;
                }
                else
                {
                    var propertiesObject = new SettingCreateViewModel
                    {
                        FromDate = settingObject.FromDate,
                        DaysRequiredInOffice = setting.DaysRequiredInOffice,
                        Holidays = setting.Holidays,
                        Workhours = setting.Workhours,
                        FutureReservationWindow = setting.FutureReservationWindow,
                        WeekendsAllowed = setting.WeekendsAllowed
                    };
                    settingObject.Settings = ConvertPropertiesToJsonString(propertiesObject);
                    UpdateDBRecord(settingObject);
                    return true;
                }
            }

            return false;

        }

        public SettingReadViewModel GetById(int id)
        {
            if (!SettingExists(id))
            {
                return null;
            }

            var setting = _context.Setting.FirstOrDefault(e => e.Id == id);
            var settingReadViewModel = ConvertSettingToReadModel(setting);

            return settingReadViewModel;
        }

        public bool DeleteSetting(int id)
        {
            if (!SettingExists(id))
            {
                return false;
            }

            var settingObject = _context.Setting.FirstOrDefault(e => e.Id == id);
            if (settingObject.FromDate <= DateTime.Now)
            {
                return false;
            }
            else
            {
                DeleteDBRecord(settingObject);
                return true;
            }


        }

        public List<SettingReadViewModel> GetAllSettings()
        {
            var settingsList = _context.Setting.ToList();
            var settingReadViewModelList = new List<SettingReadViewModel>();

            foreach (var setting in settingsList)
            {
                settingReadViewModelList.Add(ConvertSettingToReadModel(setting));
            }

            return settingReadViewModelList;
        }

        public bool IsValidSetting(SettingCreateViewModel setting)
        {
            var startHour = setting.Workhours.StartHour;
            var endHour = setting.Workhours.EndHour;
            string hourPattern = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            Regex regex = new(hourPattern);
            DateTime[] holidays = setting.Holidays;
            var daysRequiredInOffice = setting.DaysRequiredInOffice;
            var futureReservationWindow = setting.FutureReservationWindow;
            var settingFromDate = setting.FromDate;

            if (futureReservationWindow < 1)
            {
                return false;
            }

            if (daysRequiredInOffice < 0)
            {
                return false;
            }

            if (daysRequiredInOffice > 7)
            {
                return false;
            }

            if (holidays.GroupBy(x => x.Date).Any(g => g.Count() > 1))
            {
                return false;
            }

            if (!Regex.IsMatch(startHour, hourPattern))
            {
                return false;
            }

            if (!Regex.IsMatch(endHour, hourPattern))
            {
                return false;
            }

            if (setting.FromDate <= DateTime.Now)
            {
                return false;
            }

            if (setting.FromDate <= _context.Setting.OrderByDescending(s => s.FromDate).FirstOrDefault().FromDate)
            {
                return false;
            }

            return true;
        }

        public bool IsValidSetting(SettingUpdateViewModel setting)
        {
            var startHour = setting.Workhours.StartHour;
            var endHour = setting.Workhours.EndHour;
            string hourPattern = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            Regex regex = new(hourPattern);
            DateTime[] holidays = setting.Holidays;
            var daysRequiredInOffice = setting.DaysRequiredInOffice;
            var futureReservationWindow = setting.FutureReservationWindow;

            if (futureReservationWindow < 1)
            {
                return false;
            }

            if (daysRequiredInOffice < 0)
            {
                return false;
            }

            if (daysRequiredInOffice > 7)
            {
                return false;
            }

            if (holidays.GroupBy(x => x.Date).Any(g => g.Count() > 1))
            {
                return false;
            }

            if (!Regex.IsMatch(startHour, hourPattern))
            {
                return false;
            }

            if (!Regex.IsMatch(endHour, hourPattern))
            {
                return false;
            }

            return true;


        }
    }
}
