using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficePlanner.Shared
{
    public class PersonUser
    {
        [Key]
        public int Id { get; set; }
        //foreign key users
        [ForeignKey("AspNetUsers")]
        public int UserId { get; set; }
        // foreign key Persons
        [ForeignKey("Persons")]
        public int PersonId { get; set; }
    }
}
