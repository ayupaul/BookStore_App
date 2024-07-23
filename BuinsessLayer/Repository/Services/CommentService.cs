using BuisnessLayer.Data;

using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Repository.Services
{
    public class CommentService 
    {
        private readonly AppDbContext context;

        public CommentService(AppDbContext context)
        {
            this.context = context;
        }
        public void AddCommentToParticularBook(CommentViewModel model)
        {
            context.Comments.Add(model);
            context.SaveChanges();
        }
    }
}
