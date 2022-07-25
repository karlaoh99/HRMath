using HRMath.Models;
using HRMath.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HRMath.Components
{
    public class ContractProfessor : ViewComponent
    {
        IProfessorRepository professorRepository;

        public ContractProfessor(IProfessorRepository professorRepository) => this.professorRepository = professorRepository;

        public IViewComponentResult Invoke(Guid pId)
        {
            return View(new ContractProfessorModel{ Professor = pId, StartDate=DateTime.Today});
        }


    }



}