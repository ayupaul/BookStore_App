using BuisnessLayer.Data;
using BuisnessLayer.Data.Repository.IServices;
using BuisnessLayer.Facade;
using BuisnessLayer.UnitOfWork;
using DataAccessLayer.Models;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers

{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller

    {

     
        //private readonly IAdminServices adminServices;
        //private readonly IUnitOfWork unitOfWork;
        private readonly IBookReadingEventFacade bookReadingEventFacade;

        public AdminController(/*IAdminServices adminServices IUnitOfWork unitOfWork*/ IBookReadingEventFacade bookReadingEventFacade)

        {

          
            //this.adminServices = adminServices;
            //this.unitOfWork = unitOfWork;
            this.bookReadingEventFacade = bookReadingEventFacade;
        }

        public IActionResult AllEvents()

        {

            var events = bookReadingEventFacade.AllEvents();

            return View(events);

        }

        [HttpGet]

        public IActionResult CreateRole()

        {

            return View();

        }

        [HttpPost]

        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)

        {

            if (ModelState.IsValid)

            {



                IdentityResult result = await bookReadingEventFacade.CreateRole(model);

                if (result.Succeeded)

                {

                    return RedirectToAction("ListRoles", "Admin");

                }

                foreach (IdentityError error in result.Errors)

                {

                    ModelState.AddModelError("", error.Description);

                }

            }

            return View(model);

        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = bookReadingEventFacade.AllRoles();
            return View(roles);
        }
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await bookReadingEventFacade.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach(var user in await bookReadingEventFacade.GetUserInRoleAsync(role.Name))
            {
              
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await bookReadingEventFacade.GetRoleByIdAsync(model.Id);
            if (role == null)
            {
                return NotFound();
            }
            else
            {
                role.Name = model.RoleName;
                var result = await bookReadingEventFacade.UpdateRoleAsync(role);
                if (result.Succeeded) {
                    return RedirectToAction("ListRoles");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        [HttpGet]

        public async Task<IActionResult> Add(string roleId)

        {

            ViewBag.roleId = roleId;

            var role = await bookReadingEventFacade.GetRoleByIdAsync(roleId);

            var model = new List<CreateUserRoleViewModel>();

            foreach (var user in await bookReadingEventFacade.GetAllUsersAsync())

            {

                var createUserRoleViewModel = new CreateUserRoleViewModel

                {

                    UserId = user.Id,

                    UserName = user.UserName

                };

                if (await bookReadingEventFacade.IsInRoleAsync(user, role.Name))

                {

                    createUserRoleViewModel.IsSelected = true;

                }

                else

                {

                    createUserRoleViewModel.IsSelected = false;

                }

                model.Add(createUserRoleViewModel);

            }

            return View(model);

        }

        [HttpPost]

        public async Task<IActionResult> Add(List<CreateUserRoleViewModel> model, string roleId)

        {

            var role = await bookReadingEventFacade.GetRoleByIdAsync(roleId);

            for (int i = 0; i < model.Count; i++)

            {

                var user = await bookReadingEventFacade.FindUserByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await bookReadingEventFacade.IsInRoleAsync(user, role.Name)))

                {

                    result = await bookReadingEventFacade.AddToRoleAsync(user, role.Name);

                }

                else if (!model[i].IsSelected && await bookReadingEventFacade.IsInRoleAsync(user, role.Name))

                {

                    result = await bookReadingEventFacade.RemoveFromRoleAsync(user, role.Name);

                }

                else

                {

                    continue;

                }

                if (result.Succeeded)

                {

                    return RedirectToAction("ListRoles", "Admin");

                }

            }


            return View("edit");

        }

    }

}







