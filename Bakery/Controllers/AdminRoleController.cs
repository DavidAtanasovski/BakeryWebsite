using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Bakery.Models;

namespace CarWebApp.Controllers
{
  
    public class AdminRoleController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public AdminRoleController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }  

        public IActionResult AddNewRole() => View();

        [HttpPost]
        public async Task<IActionResult> AddNewRole(AddRoleViewModel addRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(addRoleViewModel);
            }

            var role = new IdentityRole
            {
                Name = addRoleViewModel.RoleName
            };

            IdentityResult result = await roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(addRoleViewModel);
        }

        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return RedirectToAction("Index");
            }

            var editRoleViewModel = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
                Users = new List<string>()
            };


            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                    editRoleViewModel.Users.Add(user.UserName);
            }

            return View(editRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel editRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(editRoleViewModel);
            }
            var role = await roleManager.FindByIdAsync(editRoleViewModel.Id);

            if (role != null)
            {
                role.Name = editRoleViewModel.RoleName;

                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("Index");

                ModelState.AddModelError("", "Role not updated, something went wrong.");

                return View(editRoleViewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Something went wrong while deleting this role.");
            }
            else
            {
                ModelState.AddModelError("", "This role can't be found.");
            }
            return View("Index", roleManager.Roles);
        }

        //Users in roles

        public async Task<IActionResult> AddUserToRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return RedirectToAction("Index");
            }

            var addUserToRoleViewModel = new UserRoleViewModel { RoleId = role.Id };
            foreach (var user in userManager.Users)
            {
                if (!await userManager.IsInRoleAsync(user, role.Name))
                {
                    addUserToRoleViewModel.Users.Add(user);
                }
            }

            return View(addUserToRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserToRole(UserRoleViewModel userRoleViewModel)
        {
            if (!ModelState.IsValid)
            {                
                var roleTemp = await roleManager.FindByIdAsync(userRoleViewModel.RoleId);
                foreach (var userItem in userManager.Users)
                {
                    if (!await userManager.IsInRoleAsync(userItem, roleTemp.Name))
                    {
                        userRoleViewModel.Users.Add(userItem);
                    }
                }
                return View(userRoleViewModel);
            }
            var user = await userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await roleManager.FindByIdAsync(userRoleViewModel.RoleId);

            var result = await userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userRoleViewModel);
        }

        public async Task<IActionResult> DeleteUserFromRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                return RedirectToAction("Index");
            }

            var addUserToRoleViewModel = new UserRoleViewModel { RoleId = role.Id };
            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    addUserToRoleViewModel.Users.Add(user);
                }
            }

            return View(addUserToRoleViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUserFromRole(UserRoleViewModel userRoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                var roleTemp = await roleManager.FindByIdAsync(userRoleViewModel.RoleId);
                foreach (var userItem in userManager.Users)
                {
                    if (!await userManager.IsInRoleAsync(userItem, roleTemp.Name))
                    {
                        userRoleViewModel.Users.Add(userItem);
                    }
                }
                return View(userRoleViewModel);
            }

            var user = await userManager.FindByIdAsync(userRoleViewModel.UserId);
            var role = await roleManager.FindByIdAsync(userRoleViewModel.RoleId);

            var result = await userManager.RemoveFromRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(userRoleViewModel);
        }
    }
}