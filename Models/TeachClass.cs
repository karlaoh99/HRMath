using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class TeachClass
    {
        public int Classroom { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan HourInit { get; set; }
        public TimeSpan HourFinish { get; set; }
        public Guid IdP { get; set; }
        public int IdSchedule { get; set; }

        public virtual Class Class { get; set; }
        public virtual Professor IdPNavigation { get; set; }
    }
}
