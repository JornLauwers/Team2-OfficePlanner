using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string RoomName { get; set; }
        public string UserName { get; set; }

    }
}
