using HRMath.Models;
using HRMath.Data;
using Microsoft.AspNetCore.Mvc;


namespace HRMath.Components
{
    public class RegisterProfessorSummary : ViewComponent
    {
        IProfessorRepository professorRepository;

        public RegisterProfessorSummary(IProfessorRepository professorRepository) => this.professorRepository = professorRepository;


        public IViewComponentResult Invoke()
        {
            return View(new RegisterProfessorModel());
        }


    }



}