using System;
using System.Linq;
using HRMath.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HRMath.Data
{
    public interface IClassRepository
    {
        IQueryable<Class> Classes {get;}

        IEnumerable<Class> GetBySchedule(int id);

    }

    public class ClassRepositoryEF : IClassRepository 
    {
        private EFDatabaseContext context;

        public ClassRepositoryEF(EFDatabaseContext context) => this.context = context;

        public IQueryable<Class> Classes => context.Class;

        public IEnumerable<Class> GetBySchedule(int id)
        {
            return Classes.Where(c => c.IdSchedule == id).Include(c => c.IdSubjectNavigation);
        }

    }



}

