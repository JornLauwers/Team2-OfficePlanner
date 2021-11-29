using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Client.Services
{
    public interface ISettingsService
    {
        Task<List<SettingReadViewModel>> GetAllSettings();
        Task<SettingReadViewModel> GetActiveSetting(DateTime date);
        Task<SettingReadViewModel> GetSettingById(int id);
        Task<bool> UpdateSetting(SettingUpdateViewModel setting, int id);
        Task<bool> DeleteSetting(int id);
        Task<bool> CreateSetting(SettingCreateViewModel setting);
    }
}
