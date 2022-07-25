using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HRMath.Models;
using HRMath.Data;
using System;

namespace HRMath.Data
{

    public interface IProfessorRepository
    {
        IQueryable<Professor> Professors { get; }

        void AddProfessor(Professor professor);

        void RemoveProfessor(Guid id);

        Professor GetById(Guid id);
    }

    public class ProfessorRepositoryEF : IProfessorRepository
    {
        private EFDatabaseContext context;

        public ProfessorRepositoryEF(EFDatabaseContext context) => this.context = context;

        public IQueryable<Professor> Professors => context.Professor;

        public Professor GetById(Guid id)
        {
            var professor = context.Professor.Find(id);
            context.Entry(professor).Collection( p => p.TeachSubject).Load();
            context.Entry(professor).Collection(p => p.Contract).Load();
            context.Entry(professor).Collection(p => p.TeachClass).Load();
            return professor;
        }
        public void AddProfessor(Professor professor)
        {
            var entity = context.Professor.Add(professor);
            context.SaveChanges();
        }

        public void RemoveProfessor(Guid id)
        {
            context.Professor.Remove(new Professor {Id = id});
            context.SaveChanges();
        }



    }


}