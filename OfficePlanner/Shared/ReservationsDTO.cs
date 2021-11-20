using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficePlanner.Shared.Utils;

namespace OfficePlanner.Shared
{
    public class ReservationsDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public int Room { get; set; }
    }
}
