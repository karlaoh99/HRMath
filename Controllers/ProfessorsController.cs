using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using HRMath.Data;
using HRMath.Models;
using System.Text.RegularExpressions;


namespace HRMath.Controllers 
{
    
    public class ProfessorsController : Controller
    {
        private IProfessorRepository professorRepository;

        public ProfessorsController(IProfessorRepository professorRepository) => this.professorRepository = professorRepository;

        public ViewResult Index() => View(new ListProfessorsModel {
            Professors = professorRepository.Professors.OrderBy(p => p.Name),
        });

        [HttpPost]
        public ViewResult Index(ListProfessorsModel model)
        {
            var result = professorRepository.Professors;
            if (model.ScientificGrade != null)
                result = result.Where(p => p.ScientificGrade == model.ScientificGrade);
            if (model.TeachingCategory != null)
                result = result.Where(p => p.TeachingCategory == model.TeachingCategory);
            if (model.Pattern != null)
            {
                string pattern = string.Join('|', model.Pattern.Split(new char[] {'+', ' '}, StringSplitOptions.RemoveEmptyEntries));
                result = result.AsEnumerable().Where(p => Regex.IsMatch(p.Name.ToLower(), pattern.ToLower(), RegexOptions.IgnorePatternWhitespace)).AsQueryable();
            }
            model.Professors = result;
            return View(model);
        }


        [HttpPost]
        public IActionResult Register(RegisterProfessorModel model)
        {
            if (ModelState.IsValid)
            {
                List<string> errors = new List<string>();

                if (professorRepository.Professors.Any(p => p.Email == model.Email))
                    errors.Add("Ya está registrado un profesor con dicho correo electrónico.");
                if (professorRepository.Professors.Any(p => p.PersonalId == model.PersonalId))
                    errors.Add("Ya está registrado un profesor con dicho carnet de identidad.");

                if (errors.Count > 0)
                    ViewBag.ErrorMsg = string.Join("\n", errors);
                else
                {
                    var fullName = $"{model.Name} {model.FirstLastName} {model.SecondLastName}";
                    ViewBag.SuccessMsg = $"Se registró satisfactoriamente un nuevo profesor: {fullName}";
                    var professor = new Professor{PersonalId=model.PersonalId, Name=fullName, Email=model.Email, Address=model.Address,
                    Cellphone=model.Cellphone, Landphone=model.Landphone, ScientificGrade=model.ScientificGrade, TeachingCategory=model.TeachingCategory};
                    professorRepository.AddProfessor(professor);
                }
                              
                return View("Index", new ListProfessorsModel {
                    Professors = professorRepository.Professors.OrderBy(p => p.Name),
                });
            }
            return View(model);
        } 


        public IActionResult Details(Guid id)
        {
            return View(professorRepository.GetById(id));
        }


        public IActionResult Remove(Guid id)
        {
            professorRepository.RemoveProfessor(id);
            return RedirectToAction ("Index");
        }

    }




}