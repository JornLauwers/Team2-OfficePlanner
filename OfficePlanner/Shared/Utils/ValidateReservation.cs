using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OfficePlanner.Shared.Utils
{
    public class ValidateReservation : IValidateReservation
    {
        private readonly HttpClient http;

        public ValidateReservation(HttpClient http)
        {
            this.http = http;
        }
        public async Task<bool> Validate(ReservationCreateViewModel reservation)
        {
            string Uri = $"IsReservationValid";

            HttpResponseMessage result = await http.PostAsJsonAsync<ReservationCreateViewModel>(Uri, reservation);

            string stringResult = await result.Content.ReadAsStringAsync();

            return stringResult == "true";

        }
    }
}
