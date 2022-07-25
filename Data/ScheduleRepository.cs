using HRMath.Models;
using System.Linq;

namespace HRMath.Data
{
    public interface IScheduleRepository
    {
        IQueryable<Schedule> Schedules {get;}

    }

    public class ScheduleRepositoryEF : IScheduleRepository
    {
        private EFDatabaseContext context;

        public ScheduleRepositoryEF(EFDatabaseContext context) => this.context = context;

        public IQueryable<Schedule> Schedules => context.Schedule;        

    }

}