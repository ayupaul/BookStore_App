using BuisnessLayer.Data.Repository.IServices;
using BuisnessLayer.Data.Repository.Services;
using BuisnessLayer.Repository.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.UnitOfWork
{
    public interface IUnitOfWork
    {
        public BookEventService BookEventService { get; }
        public CommentService CommentService { get; }
        public AdminServices AdminServices { get; }
        void Save();
    }
}
