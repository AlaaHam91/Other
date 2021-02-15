using BookStore.WebUI.Infrastructure.Abstract;
using BookStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        IAuthenticationProvider authProvider;
        public AccountController(IAuthenticationProvider auth)
        {
            authProvider = auth;
        }
        // GET: Account
        public ViewResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (authProvider.Authenticate(login.Username, login.Password))
                    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
                else
                {
                    ModelState.AddModelError("","Incorrect username and password");
                    return View();
                }
            }
            else
            return View();
        }
    }
}