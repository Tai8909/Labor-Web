using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Checklist.Security;
using Checklist.Models;
using Checklist.Controllers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Xml;

namespace Checklist.Mvc.Controllers
{
    public class SecurityController : Controller
    {
        private readonly schoolContext _schoolcontext;
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;
        private readonly SignInManager<AppIdentityUser> signinManager;

        public SecurityController(UserManager<AppIdentityUser> userManager, RoleManager<AppIdentityRole> roleManager,
            SignInManager<AppIdentityUser> signinManager, schoolContext schoolcontext)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signinManager = signinManager;
            this._schoolcontext = schoolcontext;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(Register obj)
        {
            if (ModelState.IsValid)
            {
                if (!roleManager.RoleExistsAsync("Manager").Result)
                {
                    AppIdentityRole role = new AppIdentityRole();
                    role.Name = "Manager";
                    role.Description = "Can perform CRUD operations.";
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }

                AppIdentityUser user = new AppIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.EMail;
                user.FullName = obj.FullName;
                user.BirthDate = obj.Birthday;

                IdentityResult result = userManager.CreateAsync
                (user, obj.Password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                    return RedirectToAction("SignIn", "Security");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid user details");
                }   

            }
            return View(obj);
        }


        public IActionResult SignIn()
        {
            return View();
        }

        public IActionResult Loginto(string a, string p, string id)
        {
            //string sub = "https://aca.nuk.edu.tw/webauth/check4.asp?" + "a=" + a + "&p=" + p + "&id=" + id;
            string sub = "./Index2.xml";
            String URLString = sub;
            XmlReader reader = XmlReader.Create(URLString);

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    //return only when you have START tag  
                    switch (reader.Name.ToString())
                    {
                        case "result":
                            //Console.WriteLine("The Name of the Student is " + reader.ReadString());
                            string result = reader.ReadString();
                            if(result == "N")
                            {
                                return RedirectToAction("AccessDenied", "Security");
                            }
                            else
                            {
                                TempData["card"] = result;
                                TempData["ismanager"] = GetManagement(result);
                            }
                            TempData.Keep("card");
                            TempData.Keep("ismanager");
                            break;
                    }
                }
                Console.WriteLine("");
            }
            if(Convert.ToString(TempData["card"]).Length == 8)
            {
                return RedirectToAction("Search", "Home");
            }
            else
            {
                return RedirectToAction("Search", "Home");
            }

        }

        public IActionResult SignOut()
        {
            TempData.Remove("card");
            //signinManager.SignOutAsync().Wait();
            return RedirectToAction("SignIn", "Security");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public string GetManagement(string user_no)
        {
            var result = _schoolcontext.Management_table
                .Where(a => a.User_No == user_no);
            var temp = result.ToList();
            Console.WriteLine(temp);
            if (result.Count() == 0)
            {
                return "false";
            }
            return "true";
        }
    }
}
