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

        public async Task<bool> CreateRoom(RoomsCreateViewModel room)
        {
            string uri = "api/Rooms";
            var response = await httpClient.PostAsJsonAsync(uri, room);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
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

        public async Task<RoomsReadViewModel> GetActiveVersion(int id)
        {
            string uri = $"api/rooms/active/{id}";
            return await httpClient.GetFromJsonAsync<RoomsReadViewModel>(uri);
        }

        public async Task<List<RoomsReadViewModel>> GetAllRooms(DateTime date)
        {
            string uri = $"api/rooms?date={date}";
            return await httpClient.GetFromJsonAsync<List<RoomsReadViewModel>>(uri);
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
    }
}
