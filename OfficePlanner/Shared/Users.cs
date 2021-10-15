using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficePlanner.Shared
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool Active { get; set; }

        // role foreign key
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        //navigation variable
        public Roles Roles { get; set; }
        // persons foreign key
        [ForeignKey("Persons")]
        public int PersonId { get; set; }
        //navigation variable
        public Persons Persons { get; set; }
    }
}
