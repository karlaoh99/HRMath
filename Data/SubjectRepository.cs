using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HRMath.Models;
using System;

namespace HRMath.Data
{
    public interface ISubjectRepository
    {
        IEnumerable<Subject> Subjects { get; }

        bool Add(Subject subject);
        bool Remove(int id);
        bool Update(Subject subject);
        Subject GetSubject(int id);

        bool SaveChanges();
    }

    public class EFSubjectRepository : ISubjectRepository
    {
        private EFDatabaseContext context;

        public EFSubjectRepository(EFDatabaseContext context) => this.context = context;

        public IEnumerable<Subject> Subjects => context.Subject.Include(s => s.SubjectGrade).ToList();

        public bool Add(Subject subject)
        {
            context.Subject.Add(subject);
            return SaveChanges();
        }

        public bool Remove(int id)
        {
            Subject subject = context.Subject.Find(id);
            if (subject != null)
                context.Subject.Remove(subject);
            return SaveChanges();
        }

        public bool Update(Subject newSubject)
        {
            Subject subject = GetSubject(newSubject.Id);
            subject.Name = newSubject.Name;
            subject.IsOptative = newSubject.IsOptative;
            subject.IsAnual = newSubject.IsAnual;

            //context.Entry(subject).Collection(s => s.SubjectGrade).Load();
            subject.SubjectGrade = new HashSet<SubjectGrade>();
            foreach (SubjectGrade sg in newSubject.SubjectGrade)
            {
                subject.SubjectGrade.Add(sg);
            }

            return SaveChanges();
        }

        public Subject GetSubject(int id)
        {
            Subject subject = context.Subject.Find(id);
            context.Entry(subject).Collection(s => s.SubjectGrade).Load();
            return subject;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
    }
}
