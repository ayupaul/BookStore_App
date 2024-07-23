using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.NotifyPublisher
{
    public interface IObserver
    {
        void Update(CreateBookViewModel bookData, CommentViewModel commentData);
    }
}
