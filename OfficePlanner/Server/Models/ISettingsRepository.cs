using OfficePlanner.Shared;
using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Server.Models
{
    public interface ISettingsRepository
    {
        bool CreateSetting(SettingCreateViewModel setting);
        SettingReadViewModel GetActiveProperties(DateTime date);
        SettingReadViewModel GetById(int id);
        bool UpdateSetting(SettingUpdateViewModel setting, int id);
        bool SettingExists(int id);
        bool DeleteSetting(int id);
        List<SettingReadViewModel> GetAllSettings();
        bool IsValidSetting(SettingCreateViewModel setting);
        bool IsValidSetting(SettingUpdateViewModel setting);
    }
}
