using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using HRMath.Models;
using System.Collections;
using System;

namespace HRMath.Controllers
{

    /*This controller manages the requests associated with the users accounts
     (i.e. singin, login, logout, account administration,...)*/
    public class AccountController: Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        IPasswordHasher<AppUser> _passwordHasher;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._passwordHasher = passwordHasher;
        }


        public ActionResult Index(){
            return View(new List<AppUser>(_userManager.Users));
        }
        
        
        #region SIGNIN
        public ViewResult Signup(string returnUrl) => View();
        

        [HttpPost]
        public async Task<IActionResult> Signup(SignupModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser{ UserName = model.Name, Email = model.Email };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    return Redirect(ViewBag.returnUrl ?? "/");
                } else {
                  foreach (IdentityError error in result.Errors) {
                      ModelState.AddModelError("", error.Description);
                  }  
                }
            }
            return View(model);
        }

        #endregion


        #region LOGIN

        public IActionResult Login(string returnUrl) => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //Tries to find a user in the database with the email provided by the form
                AppUser user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user != null)
                {
                    await _signInManager.SignOutAsync(); //closes a possible previous open session
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false);
                    if (result.Succeeded)
                        return Redirect(returnUrl ?? "/");
                }
                ModelState.AddModelError(nameof(loginModel.Email), "Invalid user or password");
            }
            return View(loginModel);
        }
     
        #endregion


        #region LOGOUT
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion
      

        #region DELETE
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                } else {
                    AddErrorFromResults(result);
                }
            } else {
                ModelState.AddModelError("", "User Not Found");
            }
            return View("Index", _userManager.Users);
        }

        #endregion

        private void AddErrorFromResults(IdentityResult result){
            foreach(var e in result.Errors)
                ModelState.AddModelError("", e.Description);
        }

    }

}

