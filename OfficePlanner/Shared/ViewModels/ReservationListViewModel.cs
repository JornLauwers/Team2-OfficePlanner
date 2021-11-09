using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Shared.ViewModels
{
    public class ReservationListView<ApplicationUser>
    {
        public List<Reservations<ApplicationUser>> Reservations { get; set; } = new List<Reservations<ApplicationUser>>();
    }
}
