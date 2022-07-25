using HRMath.Models;
using HRMath.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMath.Components
{
    public class AddSubjectSummary : ViewComponent
    {
        ISubjectRepository subjectRepository;
        IGradeRepository gradeRepository;

        public AddSubjectSummary(ISubjectRepository subjectRepository, IGradeRepository gradeRepository)
        {
            this.subjectRepository = subjectRepository;
            this.gradeRepository = gradeRepository;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.Grades = gradeRepository.Grades.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return View(new AddSubjectModel());
        }
    }
}