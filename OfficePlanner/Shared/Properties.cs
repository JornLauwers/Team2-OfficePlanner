﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficePlanner.Shared
{
    public class Properties
    {
        public Workhours Workhours { get; set; }
        public DateTime[] Holidays { get; set; }
        public int DaysRequiredInOffice { get; set; }
        public int FutureReservationWindow { get; set; }
        public bool WeekendsAllowed { get; set; }
    }
}
