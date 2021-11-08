using System;
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
    }

    public class Workhours
    {
        public string StartHour { get; set; }
        public string EndHour { get; set; }
    }
}
