using HRMath.Models;
using HRMath.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


namespace HRMath.Components
{
    public class SetAdminSubjectSummary : ViewComponent
    {
        ISubjectRepository subjectRepository;
        UserManager<AppUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public SetAdminSubjectSummary(ISubjectRepository subjectRepository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.subjectRepository = subjectRepository;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            Subject subject = subjectRepository.GetSubject(id);
            SetSubjectAdminModel model = new SetSubjectAdminModel
            {
                Id = id,
                Name = subject.Name
            };

            IList<AppUser> users = await _userManager.GetUsersInRoleAsync("Admin" + id); 
            ViewBag.Users = _userManager.Users.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.UserName, Selected = users.Contains(u) }).ToList();

            return View(model);
        }
    }
}
