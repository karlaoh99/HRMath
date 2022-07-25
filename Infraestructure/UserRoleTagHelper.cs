using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;
using HRMath.Models;
using System.Threading.Tasks;

namespace HRMath.Infraestructure{

    [HtmlTargetElement("td", Attributes = "identity-user")]
    public class UserRolesTagHelper : TagHelper
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public UserRolesTagHelper(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        [HtmlAttributeName("identity-user")]
        public string UserId {get; set; }

       
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            AppUser user = await _userManager.FindByIdAsync(UserId);
            if (user != null){
                var roles = await _userManager.GetRolesAsync(user);
                string encoded = "";
                foreach (var r in roles)
                    encoded += $"<span class=\"badge badge-info\">{r}</span>";
                output.Content.SetHtmlContent(encoded);
            } else {
                output.Content.SetHtmlContent($"<span class=\"badge badge-danger\">No Roles</span>");
            }
        }

    }

}