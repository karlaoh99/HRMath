using HRMath.Models;
using HRMath.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HRMath.Components
{
    public class EditSubjectSummary : ViewComponent
    {
        ISubjectRepository subjectRepository;
        IGradeRepository gradeRepository;

        public EditSubjectSummary(ISubjectRepository subjectRepository, IGradeRepository gradeRepository)
        {
            this.subjectRepository = subjectRepository;
            this.gradeRepository = gradeRepository;
        }

        public IViewComponentResult Invoke(int id)
        {
            Subject subject = subjectRepository.GetSubject(id);
            AddSubjectModel model = new AddSubjectModel
            {
                Id = subject.Id,
                Name = subject.Name,
                IsAnual = subject.IsAnual,
                IsOptative = subject.IsOptative
            };

            List<int> grades = new List<int>();
            foreach (var selected in subject.SubjectGrade)
                grades.Add(selected.IdG);
            ViewBag.Grades = gradeRepository.Grades.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name, Selected = grades.Contains(r.Id) }).ToList();
            
            return View(model);
        }
    }
}