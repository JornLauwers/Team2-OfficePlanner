using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficePlanner.Shared.ViewModels
{
    public class RoomsReadViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AvailableSeats { get; set; }
        public int FreeSeats { get; set; }
        public int Id { get; set; }
    }
}
