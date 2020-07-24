using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RugerRumble.Models;

namespace RugerRumble.Controllers
{
    public class FullFillmentController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private AppDbContext context;

        public FullFillmentController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager,AppDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetSystemReady()
        {
            string email = "sysadmin@admin.com";
            string password = "System0909";
            var checkUser =await userManager.FindByEmailAsync(email);
            if (checkUser==null)
            {
                //user create
                IdentityRole model = new IdentityRole();
                model.Name = "Admin";
                await roleManager.CreateAsync(model);
                ApplicationUser user = new ApplicationUser();
                user.FullName = "System Admin";
                user.Email = email;
                user.UserName = "sysadmin@admin.com";
                var result = await userManager.CreateAsync(user,password);
                //create end
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    await userManager.ConfirmEmailAsync(user, token);
                    IdentityRole model2 = new IdentityRole();
                    model2.Name = "Client";
                    await roleManager.CreateAsync(model2);
                    var results = await signInManager.PasswordSignInAsync(user, password, false, false);
                    if (results.Succeeded)
                    {
                        ViewBag.Message = "System is ready for use.";
                    }


                    //product create
                    ProductP p1 = new ProductP();
                    p1.ProductName = "Ruger Rumble";
                    p1.Price = 2.99;
                    p1.ImgUrl = "\\Icons\\Ruger Rumble.png";
                    context.Add(p1);
                }
            }
            else
            {
                ViewBag.Message = "System is already configured.";

            }
            return View();
        }
    }
}
