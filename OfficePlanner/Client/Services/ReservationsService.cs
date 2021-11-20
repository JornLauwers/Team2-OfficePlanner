using OfficePlanner.Shared;
using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace OfficePlanner.Client.Services
{
    public class ReservationsService : IReservationsService
    {
        private readonly HttpClient httpClient;

        public ReservationsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<bool> CreateReservation(ReservationCreateViewModel reservationCreateViewModel)
        {
            string uri = "api/reservations/CreateReservation";
            var response = await httpClient.PostAsJsonAsync<ReservationCreateViewModel>(uri, reservationCreateViewModel);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteReservation(int id)
        {
            string uri = $"api/Reservations/DeleteReservation/{id}";
            var response = await httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        public async Task<ReservationListView> GetReservationByDate(DateTime startDate, DateTime endDate)
        {
            string uri = $"api/Reservations/GetReservationByDate?startDate={startDate.ToString()}&endDate={endDate.ToString()}";
            return await httpClient.GetFromJsonAsync<ReservationListView>(uri);
        }

        public async Task<ReservationsDTO> GetReservationById(int id)
        {
            string uri = $"api/Reservations/GetReservationById/{id}";
            return await httpClient.GetFromJsonAsync<ReservationsDTO>(uri);
        }

        public Task UpdateReservation(ReservationUpdateViewModel reservationUpdateViewModel)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Validate(ReservationCreateViewModel reservation)
        {
            string Uri = $"IsReservationValid";

            HttpResponseMessage result = await httpClient.PostAsJsonAsync<ReservationCreateViewModel>(Uri, reservation);

            string stringResult = await result.Content.ReadAsStringAsync();

            return stringResult == "true";

        }
    }
}
