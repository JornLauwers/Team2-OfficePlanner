using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficePlanner.Shared
{
    public class Rooms<ApplicationUser>
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
        public ICollection<Reservations<ApplicationUser>> Reservations { get; set; }
        public ICollection<RoomVersions<ApplicationUser>> RoomVersions { get; set; }
    }
}
