using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Shared.ViewModels
{
    public class ReservationListView
    {
        public List<ReservationsDTO> Reservations { get; set; } = new List<ReservationsDTO>();
    }
}
