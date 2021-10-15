using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficePlanner.Shared
{
    public class Reservations
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        //foreign key users
        [ForeignKey("Users")]
        public int User { get; set; }
        public Users Users { get; set; }
        // ferign key rooms
        [ForeignKey("Rooms")]
        public int Room { get; set; }
        public Rooms Rooms { get; set; }
    }
}
