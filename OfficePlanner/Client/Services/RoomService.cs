using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OfficePlanner.Client.Services
{
    public class RoomService : IRoomService
    {
        private readonly HttpClient httpClient;

        public RoomService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<int> CreateRoom(RoomsCreateViewModel room)
        {
            string uri = "api/Rooms";
            var response = await httpClient.PostAsJsonAsync(uri, room);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return Int32.Parse(result);
            }
            return 0;
        }

        public async Task<bool> CreateRoomVersion(RoomVersionsCreateViewModel roomVersion, int id)
        {
            string uri = $"api/Rooms/addversion/{id}";
            var response = await httpClient.PostAsJsonAsync(uri, roomVersion);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteRoomVersion(int id)
        {
            string uri = $"api/Rooms/Versions/{id}";
            var response = await httpClient.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<RoomsReadViewModel> GetActiveVersion(int id)
        {
            string uri = $"api/Rooms/active/{id}";
            return await httpClient.GetFromJsonAsync<RoomsReadViewModel>(uri);
        }

        public async Task<List<RoomsReadViewModel>> GetAllRooms(DateTime date)
        {
            string uri = $"api/Rooms?date={date}";
            return await httpClient.GetFromJsonAsync<List<RoomsReadViewModel>>(uri);
        }

        public async Task<List<RoomVersionsReadViewModel>> GetAllVersions(int id)
        {
            string uri = $"api/Rooms/Versions/{id}";
            return await httpClient.GetFromJsonAsync<List<RoomVersionsReadViewModel>>(uri);
        }

        public async Task<RoomVersionsReadViewModel> GetVersionById(int id)
        {
            string uri = $"api/Rooms/VersionDetails/{id}";
            return await httpClient.GetFromJsonAsync<RoomVersionsReadViewModel>(uri);
        }

        public async Task<bool> UpdateRoom(RoomsCreateViewModel room, int id)
        {
            string uri = $"api/Rooms/{id}";
            var response = await httpClient.PutAsJsonAsync(uri, room);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateRoomVersion(RoomVersionsCreateViewModel roomVersion, int id)
        {
            string uri = $"api/Rooms/Versions/{id}";
            var response = await httpClient.PutAsJsonAsync(uri, roomVersion);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
