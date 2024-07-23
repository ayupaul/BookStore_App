using BuisnessLayer.Data;
using BuisnessLayer.Data.Repository.IServices;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Data.Repository.Services
{
    public class AdminServices : IAdminServices
    {
        private readonly AppDbContext context;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<IdentityUser> userManager;

        public AdminServices(AppDbContext context,RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public IEnumerable<CreateBookViewModel> AllEvents()
        {
            var book = context.CreateBooks.ToList();
            return book;
        }

        public async Task<IdentityResult> CreateRole(CreateRoleViewModel model)
        {
            IdentityRole identityRole = new IdentityRole

            {

                Name = model.RoleName

            };
            IdentityResult result = await roleManager.CreateAsync(identityRole);
            return result;
        }
        public IEnumerable<IdentityRole> AllRoles()
        {
            var roles = roleManager.Roles;
            return roles;
        }

        public async Task<IdentityRole> GetRoleByIdAsync(string id)
        {
            return await roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> UpdateRoleAsync(IdentityRole role)
        {
            return await roleManager.UpdateAsync(role);
        }

        public async Task<IList<IdentityUser>> GetUserInRoleAsync(string roleName)
        {
            return await userManager.GetUsersInRoleAsync(roleName);
        }

        public async Task<IdentityUser> FindUserByIdAsync(string userId)
        {
            return await userManager.FindByIdAsync(userId);
        }
        public async Task<IdentityResult> AddToRoleAsync(IdentityUser user, string roleName)
        {
            return await userManager.AddToRoleAsync(user, roleName);
        }
        public async Task<IdentityResult> RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            return await userManager.RemoveFromRoleAsync(user, roleName);
        }
        public async Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            return await userManager.IsInRoleAsync(user, roleName);
        }
        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {
            return await userManager.Users.ToListAsync();
        }

    }
}
