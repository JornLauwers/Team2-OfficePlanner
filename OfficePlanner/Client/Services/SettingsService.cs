using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OfficePlanner.Client.Services
{
    public class SettingsService : ISettingsService
    {

        private readonly HttpClient httpClient;

        public SettingsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> CreateSetting(SettingCreateViewModel setting)
        {
            string uri = "api/settings";
            var response = await httpClient.PostAsJsonAsync(uri, setting);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteSetting(int id)
        {
            string uri = $"api/settings/{id}";
            var response = await httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<SettingReadViewModel> GetActiveSetting(DateTime date)
        {
            string uri = $"api/settings/Active?date={date}";
            return await httpClient.GetFromJsonAsync<SettingReadViewModel>(uri);
        }

        public async Task<List<SettingReadViewModel>> GetAllSettings()
        {
            string uri = $"api/settings/all";
            return await httpClient.GetFromJsonAsync<List<SettingReadViewModel>>(uri);
        }

        public async Task<SettingReadViewModel> GetSettingById(int id)
        {
            string uri = $"api/settings/{id}";
            return await httpClient.GetFromJsonAsync<SettingReadViewModel>(uri);
        }

        public async Task<bool> UpdateSetting(SettingUpdateViewModel setting, int id)
        {
            string uri = $"api/settings/{id}";
            var response = await httpClient.PutAsJsonAsync(uri, setting);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
