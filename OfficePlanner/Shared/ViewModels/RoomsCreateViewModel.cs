using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficePlanner.Shared.ViewModels
{
    public class RoomsCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
