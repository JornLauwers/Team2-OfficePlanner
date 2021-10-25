using OfficePlanner.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Shared.ViewModels
{
    public class ReservationCreateViewModel
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int User { get; set; }
        [Required]
        public int Room { get; set; }
    }
}
