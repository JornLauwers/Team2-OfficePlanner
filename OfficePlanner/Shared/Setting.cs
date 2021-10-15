using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OfficePlanner.Shared
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime UntilDate { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Settings { get; set; }
    }
}
