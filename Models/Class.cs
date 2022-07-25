using System;
using System.Collections.Generic;

namespace HRMath.Models
{
    public partial class Class
    {
        public Class()
        {
            TeachClass = new HashSet<TeachClass>();
        }

        public int Classroom { get; set; }
        public string DayOfWeek { get; set; }
        public TimeSpan HourInit { get; set; }
        public TimeSpan HourFinish { get; set; }
        public int IdSubject { get; set; }
        public int IdM { get; set; }
        public int IdSchedule { get; set; }

        public virtual Modality IdMNavigation { get; set; }
        public virtual Schedule IdScheduleNavigation { get; set; }
        public virtual Subject IdSubjectNavigation { get; set; }
        public virtual ICollection<TeachClass> TeachClass { get; set; }
    }
}
