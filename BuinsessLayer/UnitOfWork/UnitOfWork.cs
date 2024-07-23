using BuisnessLayer.Data;
using BuisnessLayer.Data.Repository.IServices;
using BuisnessLayer.Data.Repository.Services;
using BuisnessLayer.Repository.Services;
using BuisnessLayer.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private BookEventService bookEventService;
        private CommentService commentService;
        private AdminServices adminServices;
        public BookEventService BookEventService
        {
            get
            {
                if (bookEventService == null)
                {
                    bookEventService= new BookEventService(context);
                }

                return bookEventService;
            }
        }
        public CommentService CommentService
        {
            get
            {
                if (commentService == null)
                {
                    commentService = new CommentService(context);
                }

                return commentService;
            }
        }
        public AdminServices AdminServices
        {
            get
            {
                if (adminServices == null)
                {
                    adminServices = new AdminServices(context,roleManager,userManager);
                }

                return adminServices;
            }
        }


        public UnitOfWork(AppDbContext context,UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public void Save()
        {
            context.SaveChanges();
        }
    }
}
