﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficePlanner.Shared.ViewModels
{
    public class SettingReadViewModel
    {
        [Required]
        public Workhours Workhours { get; set; }
        [Required]
        public DateTime[] Holidays { get; set; }
        [Required]
        public int DaysRequiredInOffice { get; set; }
    }
}