using System;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.IdentityModel.Tokens;
using ToDoListApp.Models;
using ToDoListApp.Models.ViewModels;

namespace ToDoListApp.Controllers
{
    public class HomeController(ILogger<HomeController> logger, UserManager<AppAddFullUserName> userManager, SignInManager<AppAddFullUserName> signInManager, AppDbContext context) : Controller
    {








        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //kayýt
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> SignUp(SignUpViewModels model)
        {

            if (!ModelState.IsValid) { return View(model); }

            var userToCreate = new AppAddFullUserName()
            {
                UserName = model.Email,
                Email = model.Email,
                FullName = model.FullName


            };
            var result = await userManager.CreateAsync(userToCreate, model.Password);

            if (!result.Succeeded)
            {

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return RedirectToAction(nameof(SignIn));



        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModels model)
        {

            if (!ModelState.IsValid) { return View(model); }


            var hasUser = await userManager.FindByEmailAsync(model.Email);

            if (hasUser is null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password Wrong !!");
                return View(model);
            }

            var result = await signInManager.PasswordSignInAsync(hasUser, model.Password, true, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or Password Wrong !!");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(SignIn));

        }

        public async Task<IActionResult> Index()
        {
            var userMail = User.Identity!.Name;
            var items = await context.DoListDbs
     .Where(x => x.Username == userMail)
     .OrderByDescending(x => x.id) 
     .ToListAsync();

            return View(items);
        }


        [HttpPost]
        public async Task<IActionResult> AddTolist(string Container, int id)
        {
            var loginuser = User.Identity!.Name;
           

            var todolistCreate = new DoListDb
            {
                Username = loginuser,
                Container = Container
            };

            context.DoListDbs.Add(todolistCreate);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public async Task<IActionResult> EditandSave(int itemId, string newContainer)
        {

            var editId = context.DoListDbs.Find(itemId);

            if (editId == null)
            {
                return RedirectToAction(nameof(SignIn));
            }

            var action = Request.Form["action"];


            if (action == "save")
            {
                editId.Container = newContainer; // Burada yeni deðeri atýyoruz
                await context.SaveChangesAsync(); // Deðiþiklikleri kaydediyoruz
            }
            else if (action == "delete")
            {

                context.DoListDbs.Remove(editId);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task <IActionResult> ChangeState(int stateId)
        {
            var action=Request.Form["action"];

            var state=context.DoListDbs.Find(stateId);

            if(state is null)
            {
                return RedirectToAction(nameof(SignIn));
            }
            if (action == "success")
            {
                state.durum = 3;
                await context.SaveChangesAsync();

            }
            else if(action== "danger")
            {
                state.durum = 2;
                await context.SaveChangesAsync();   
            }
            else if(action == "warning")
            {
                state.durum = 1;
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }








    }
}
