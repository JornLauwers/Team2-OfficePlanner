using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficePlanner.Shared
{
    public class RoomVersions<ApplicationUser>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int AvailableSeats { get; set; }
        // foreign key rooms
        [ForeignKey("Rooms")]
        public int RoomId { get; set; }
        // navigation variable
        public Rooms<ApplicationUser> Rooms { get; set; }
    }
}
