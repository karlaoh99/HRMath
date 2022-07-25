using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HRMath.Models;

namespace HRMath.Data
{
    public partial class EFDatabaseContext : DbContext
    {

        public EFDatabaseContext(DbContextOptions<EFDatabaseContext> options)
            : base(options)
        {
        }

       public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Modality> Modality { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubjectGrade> SubjectGrade { get; set; }
        public virtual DbSet<TeachClass> TeachClass { get; set; }
        public virtual DbSet<TeachSubject> TeachSubject { get; set; }

     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>(entity =>
            {
                entity.HasKey(e => new { e.Classroom, e.DayOfWeek, e.HourInit, e.HourFinish, e.IdSchedule })
                    .HasName("PK__tmp_ms_x__C97D8812C442186E");

                entity.Property(e => e.DayOfWeek)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdMNavigation)
                    .WithMany(p => p.Class)
                    .HasForeignKey(d => d.IdM)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Class_ToModality");

                entity.HasOne(d => d.IdScheduleNavigation)
                    .WithMany(p => p.Class)
                    .HasForeignKey(d => d.IdSchedule)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Class_ToSchedule");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.Class)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Class_ToSubject");
            });

            modelBuilder.Entity<Contract>(entity =>
            {
                entity.HasKey(e => new { e.IdP, e.IdF, e.StartDate })
                    .HasName("PK__tmp_ms_x__2E9179EA4EA41AE5");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdFNavigation)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.IdF)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_ToFaculty");

                entity.HasOne(d => d.IdPNavigation)
                    .WithMany(p => p.Contract)
                    .HasForeignKey(d => d.IdP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contract_ToProfessor");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();

                entity.HasOne(d => d.IdFNavigation)
                    .WithMany(p => p.Grade)
                    .HasForeignKey(d => d.IdF)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Grade_ToFaculty");
            });

            modelBuilder.Entity<Modality>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Professo__A9D10534A9333BFE")
                    .IsUnique();

                entity.HasIndex(e => e.PersonalId)
                    .HasName("UQ__Professo__9C86DE36EA248207")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).IsRequired();

                entity.Property(e => e.Cellphone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Landphone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.PersonalId)
                    .IsRequired()
                    .HasColumnName("Personal Id")
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ScientificGrade)
                    .IsRequired()
                    .HasColumnName("Scientific Grade")
                    .HasMaxLength(50);

                entity.Property(e => e.TeachingCategory)
                    .IsRequired()
                    .HasColumnName("Teaching Category")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();
            });

            modelBuilder.Entity<SubjectGrade>(entity =>
            {
                entity.HasKey(e => new { e.IdS, e.IdG })
                    .HasName("PK__SubjectG__28DF60138BB2F7DA");

                entity.HasOne(d => d.IdGNavigation)
                    .WithMany(p => p.SubjectGrade)
                    .HasForeignKey(d => d.IdG)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubjectGrade_ToGrade");

                entity.HasOne(d => d.IdSNavigation)
                    .WithMany(p => p.SubjectGrade)
                    .HasForeignKey(d => d.IdS)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SubjectGrade_ToSubject");
            });

            modelBuilder.Entity<TeachClass>(entity =>
            {
                entity.HasKey(e => new { e.Classroom, e.DayOfWeek, e.HourInit, e.HourFinish, e.IdSchedule, e.IdP })
                    .HasName("PK__tmp_ms_x__A97CF45B8B837EC6");

                entity.Property(e => e.DayOfWeek)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.IdPNavigation)
                    .WithMany(p => p.TeachClass)
                    .HasForeignKey(d => d.IdP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TeachClass_ToProfessor");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TeachClass)
                    .HasForeignKey(d => new { d.Classroom, d.DayOfWeek, d.HourInit, d.HourFinish, d.IdSchedule })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TeachClass_ToClass");
            });

            modelBuilder.Entity<TeachSubject>(entity =>
            {
                entity.HasKey(e => new { e.IdP, e.IdS, e.Year, e.Semester, e.Modality })
                    .HasName("PK__TeachSub__18079DB63EA75E51");

                entity.Property(e => e.Modality)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.IdPNavigation)
                    .WithMany(p => p.TeachSubject)
                    .HasForeignKey(d => d.IdP)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeachSubject_ToProfessor");

                entity.HasOne(d => d.IdSNavigation)
                    .WithMany(p => p.TeachSubject)
                    .HasForeignKey(d => d.IdS)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeachSubject_ToSubject");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
