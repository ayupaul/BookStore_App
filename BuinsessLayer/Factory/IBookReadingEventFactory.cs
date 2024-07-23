using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer
{
    public interface IBookReadingEventFactory
    {
        CreateBookViewModel Create(int id, string title, DateTime date, string location, DateTime startTime, string eventType, int durationInHours, string description, string otherDetails, string inviteByEmail, string bookId, ApplicationUser user, ICollection<CommentViewModel> comment);
    }
}
