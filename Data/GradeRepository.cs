using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HRMath.Models;
using System;

namespace HRMath.Data
{
    public interface IGradeRepository
    {
        IEnumerable<Grade> Grades { get; }

        bool Add(Grade subject);
        bool Remove(int id);
        bool Update(Grade grade);
        Grade GetGrade(int id);
    }

    public class EFGradeRepository : IGradeRepository
    {
        private EFDatabaseContext context;

        public EFGradeRepository(EFDatabaseContext context) => this.context = context;

        public IEnumerable<Grade> Grades => context.Grade;

        public bool Add(Grade grade)
        {
            context.Grade.Add(grade);
            return context.SaveChanges() > 0;
        }

        public bool Remove(int id)
        {
            Grade grade = context.Grade.Find(id);
            if (grade != null)
                context.Grade.Remove(grade);
            return context.SaveChanges() > 0;
        }

        public bool Update(Grade newGrade)
        {
            var grade = context.Grade.Attach(newGrade);
            grade.State = EntityState.Modified;
            return context.SaveChanges() > 0;
        }

        public Grade GetGrade(int id)
        {
            return context.Grade.Find(id);
        }
    }
}
