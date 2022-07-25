using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HRMath.Data;
using HRMath.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using System;
using Microsoft.AspNetCore.Identity;


namespace HRMath.Controllers
{
    public class SubjectController : Controller
    {
        private ISubjectRepository subjectRepository;
        private IGradeRepository gradeRepository;
        private IFacultyRepository facultyRepository;
        UserManager<AppUser> _userManager;
        RoleManager<IdentityRole> _roleManager;


        public SubjectController(ISubjectRepository subjectRepository, IGradeRepository gradeRepository, IFacultyRepository facultyRepository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.subjectRepository = subjectRepository;
            this.gradeRepository = gradeRepository;
            this.facultyRepository = facultyRepository;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }


        private async Task<IEnumerable<ListSubject>> FormatSubjects(IEnumerable<Subject> subjects)
        {
            List<ListSubject> formattedSubjects = new List<ListSubject>();
            foreach (Subject s in subjects)
            {
                ListSubject subject = new ListSubject();
                subject.Id = s.Id;
                subject.Name = s.Name;
                if (s.IsOptative)
                    subject.IsOptative = "Sí";
                else
                    subject.IsOptative = "No";
                if (s.IsAnual)
                    subject.IsAnual = "Sí";
                else
                    subject.IsAnual = "No";

                subject.Grades = new List<string>();
                foreach (SubjectGrade grade in s.SubjectGrade)
                {
                    subject.Grades.Add(gradeRepository.GetGrade(grade.IdG).Name);
                }

                subject.Admins = new List<string>();
                IList<AppUser> users = await _userManager.GetUsersInRoleAsync("Admin" + s.Id);
                foreach (var user in users)
                    subject.Admins.Add(user.UserName);

                formattedSubjects.Add(subject);
            }
            return formattedSubjects;
        }

        public async Task<ViewResult> Index()
        {
            IEnumerable<Subject> subjects = subjectRepository.Subjects;

            ViewBag.Grades = gradeRepository.Grades;
            ViewBag.Faculties = facultyRepository.Faculties;
            return View(new ListSubjectsModel { Subjects = await FormatSubjects(subjects) });
        }

        [HttpPost]
        public async Task<ViewResult> Index(ListSubjectsModel model)
        {
            var result = subjectRepository.Subjects;
            if (model.Faculty != -1)
                result = result.Where(p =>
                {
                    foreach (SubjectGrade grade in p.SubjectGrade)
                    {
                        if (model.Faculty == gradeRepository.GetGrade(grade.IdG).IdF)
                            return true;
                    }
                    return false;
                });
            if (model.Grade != -1)
                result = result.Where(p =>
                {
                    foreach (SubjectGrade grade in p.SubjectGrade)
                    {
                        if (model.Grade == gradeRepository.GetGrade(grade.IdG).Id)
                            return true;
                    }
                    return false;
                });
            if (model.Pattern != null)
            {
                string pattern = string.Join('|', model.Pattern.Split(new char[] { '+', ' ' }, StringSplitOptions.RemoveEmptyEntries));
                result = result.AsEnumerable().Where(p => Regex.IsMatch(p.Name.ToLower(), pattern.ToLower(), RegexOptions.IgnorePatternWhitespace)).AsQueryable();
            }
            ViewBag.Grades = gradeRepository.Grades;
            ViewBag.Faculties = facultyRepository.Faculties;
            model.Subjects = await FormatSubjects(result);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddSubject(AddSubjectModel model)
        {
            if (ModelState.IsValid)
            {
                Subject subject = new Subject
                {
                    Name = model.Name,
                    IsOptative = model.IsOptative,
                    IsAnual = model.IsAnual
                };
                subjectRepository.Add(subject);
                foreach (int grade in model.GradesId)
                {
                    subject.SubjectGrade.Add(new SubjectGrade { IdS = subject.Id, IdG = grade });
                }
                subjectRepository.SaveChanges();
                await _roleManager.CreateAsync(new IdentityRole("Admin" + subject.Id));
                return RedirectToAction("Index");
            }

            ViewBag.Grades = gradeRepository.Grades.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return View(model);
        }


        [HttpPost]
        public IActionResult EditSubject(AddSubjectModel model)
        {
            if (ModelState.IsValid)
            {
                Subject subject = new Subject
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsOptative = model.IsOptative,
                    IsAnual = model.IsAnual
                };

                foreach (int grade in model.GradesId)
                {
                    subject.SubjectGrade.Add(new SubjectGrade { IdS = subject.Id, IdG = grade });
                }

                subjectRepository.Update(subject);
                return RedirectToAction("Index");
            }

            ViewBag.Grades = gradeRepository.Grades.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name }).ToList();
            return View(model);
        }


        public async Task<IActionResult> RemoveSubject(int id)
        {
            subjectRepository.Remove(id);
            IdentityRole role = await _roleManager.FindByNameAsync("Admin" + id);
            await _roleManager.DeleteAsync(role);
               
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> SetSubjectAdmin(SetSubjectAdminModel model)
        {
            IList<AppUser> users = await _userManager.GetUsersInRoleAsync("Admin" + model.Id);
            foreach (var user in users)
                await _userManager.RemoveFromRoleAsync(user, "Admin" + model.Id);

            foreach (var id in model.AdminsId)
            {
                AppUser user = await _userManager.FindByIdAsync(id);
                await _userManager.AddToRoleAsync(user, "Admin" + model.Id);
            }

            return RedirectToAction("Index");
        }
    }
}
