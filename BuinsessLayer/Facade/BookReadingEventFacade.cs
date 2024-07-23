using BuisnessLayer.UnitOfWork;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Facade
{
    public class BookReadingEventFacade:IBookReadingEventFacade
    {
        private readonly IUnitOfWork unitOfWork;

        public BookReadingEventFacade(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public void CreateBook(CreateBookViewModel model)
        {
            unitOfWork.BookEventService.CreateBook(model);
        }
        public (CreateBookViewModel bookReadingEvent, bool isUpcoming) EditAsync(int id)
        {
            return unitOfWork.BookEventService.EditAsync(id);
        }
        public IEnumerable<CreateBookViewModel> GetAllBooks()
        {
            return unitOfWork.BookEventService.GetAllBooks();
        }
        public List<CommentViewModel> GetAllCommentsForParticularBook(int id)
        {
            return unitOfWork.BookEventService.GetAllCommentsForParticularBook(id);
        }
        public void AddCommentToParticularBook(CommentViewModel model)
        {
            unitOfWork.CommentService.AddCommentToParticularBook(model);
        }
        public IEnumerable<CreateBookViewModel> InvitedTo(string userEmail)
        {
            return unitOfWork.BookEventService.InvitedTo(userEmail);
        }
        public List<CreateBookViewModel> MyEvents(string userId)
        {
            return unitOfWork.BookEventService.MyEvents(userId);
        }
        public CreateBookViewModel Update(CreateBookViewModel model)
        {
            return unitOfWork.BookEventService.Update(model);
        }
        public CreateBookViewModel ViewBook(int id)
        {
            return unitOfWork.BookEventService.ViewBook(id);
        }
        
        public IEnumerable<CreateBookViewModel> AllEvents()
        {
            return unitOfWork.AdminServices.AllEvents();
        }
        public async Task<IdentityResult> CreateRole(CreateRoleViewModel model)
        {
            return await unitOfWork.AdminServices.CreateRole(model);
        }
        public IEnumerable<IdentityRole> AllRoles()
        {
            return unitOfWork.AdminServices.AllRoles();
        }
        public async Task<IdentityRole> GetRoleByIdAsync(string id)
        {
            return await unitOfWork.AdminServices.GetRoleByIdAsync(id);
        }
        public async Task<IdentityResult> UpdateRoleAsync(IdentityRole role)
        {
            return await unitOfWork.AdminServices.UpdateRoleAsync(role);
        }
        public async Task<IList<IdentityUser>> GetUserInRoleAsync(string roleName)
        {
            return await unitOfWork.AdminServices.GetUserInRoleAsync(roleName);
        }
        public async Task<IdentityUser> FindUserByIdAsync(string userId)
        {
            return await unitOfWork.AdminServices.FindUserByIdAsync(userId);
        }
        public async Task<IdentityResult> AddToRoleAsync(IdentityUser user, string roleName)
        {
            return await unitOfWork.AdminServices.AddToRoleAsync(user, roleName);
        }
        public async Task<IdentityResult> RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            return await unitOfWork.AdminServices.RemoveFromRoleAsync(user, roleName);
        }
        public async Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            return await unitOfWork.AdminServices.IsInRoleAsync(user, roleName);
        }
        public async Task<List<IdentityUser>> GetAllUsersAsync()
        {
            return await unitOfWork.AdminServices.GetAllUsersAsync();
        }
    }
}
