using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficePlanner.Shared.ViewModels
{
    public class RoomVersionsCreateViewModel
    {
        public DateTime StartDate { get; set; }
        [Required]
        public int AvailableSeats { get; set; }
    }
}
