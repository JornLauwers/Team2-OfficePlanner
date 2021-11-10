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

        public Task CreateReservation(ReservationCreateViewModel reservationCreateViewModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteReservation(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ReservationListView> GetReservationByDate(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public async Task<ReservationsDTO> GetReservationById(int id)
        {
            return await httpClient.GetFromJsonAsync<ReservationsDTO>($"api/Reservations/GetReservationById/{id}");
        }

        public Task UpdateReservation(ReservationUpdateViewModel reservationUpdateViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
