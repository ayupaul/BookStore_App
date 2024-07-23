using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;



namespace BuisnessLayer.Data.Repository.IServices
{
    public interface IAdminServices
    {
        IEnumerable<CreateBookViewModel> AllEvents();
        Task<IdentityResult> CreateRole(CreateRoleViewModel model);
        IEnumerable<IdentityRole> AllRoles();
        Task<IdentityRole> GetRoleByIdAsync(string id);
        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
        Task<IList<IdentityUser>> GetUserInRoleAsync(string roleName);
        Task<IdentityUser> FindUserByIdAsync(string userId);
        Task<IdentityResult> AddToRoleAsync(IdentityUser user, string roleName);
        Task<IdentityResult> RemoveFromRoleAsync(IdentityUser user, string roleName);
        Task<bool> IsInRoleAsync(IdentityUser user, string roleName);
        Task<List<IdentityUser>> GetAllUsersAsync();
    }
}
