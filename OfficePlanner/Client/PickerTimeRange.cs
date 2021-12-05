using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Client
{
    public class PickerTimeRange
    {
        public int roomId;
        public TimeSpan min;
        public TimeSpan max;
        public TimeSpan pick;
    }
}
