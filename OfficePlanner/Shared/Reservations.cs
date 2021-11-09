using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficePlanner.Shared
{
    public class Reservations<ApplicationUser>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        //foreign key users
        [ForeignKey("Users")]
        public string User { get; set; }
        public virtual ApplicationUser Users { get; set; }
        // foreign key rooms
        [ForeignKey("Rooms")]
        public int Room { get; set; }
        public Rooms<ApplicationUser> Rooms { get; set; }
    }
}
