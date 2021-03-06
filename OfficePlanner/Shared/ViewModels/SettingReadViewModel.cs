using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficePlanner.Shared.ViewModels
{
    public class SettingReadViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Workhours Workhours { get; set; }
        [Required]
        public DateTime[] Holidays { get; set; }
        [Required]
        public int DaysRequiredInOffice { get; set; }
        [Required]
        public int FutureReservationWindow { get; set; }
        [Required]
        public bool WeekendsAllowed { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime UntilDate { get; set; }
    }
}
