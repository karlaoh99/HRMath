using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HRMath.Models;
using System;

namespace HRMath.Data
{
    public interface IFacultyRepository
    {
        IEnumerable<Faculty> Faculties { get; }

        bool Add(Faculty faculty);
        bool Remove(int id);
        bool Update(Faculty faculty);
        Faculty GetFaculty(int id);
    }

    public class EFFacultyRepository : IFacultyRepository
    {
        private EFDatabaseContext context;

        public EFFacultyRepository(EFDatabaseContext context) => this.context = context;

        public IEnumerable<Faculty> Faculties => context.Faculty;

        public bool Add(Faculty faculty)
        {
            context.Faculty.Add(faculty);
            return context.SaveChanges() > 0;
        }

        public bool Remove(int id)
        {
            Faculty faculty = context.Faculty.Find(id);
            if (faculty != null)
                context.Faculty.Remove(faculty);
            return context.SaveChanges() > 0;
        }

        public bool Update(Faculty newFac)
        {
            var faculty = context.Faculty.Attach(newFac);
            faculty.State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public Faculty GetFaculty(int id)
        {
            return context.Faculty.Find(id);
        }
    }
}
