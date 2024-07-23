using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Factory
{
    public class BookReadingEventFactory : IBookReadingEventFactory
    {
        public CreateBookViewModel Create(int id, string title, DateTime date, string location, DateTime startTime, string eventType, int durationInHours, string description, string otherDetails, string inviteByEmail, string bookId, ApplicationUser user, ICollection<CommentViewModel> comment)
        {
            return new CreateBookViewModel {
                Id = id,
                Title=title,
                Date=date,
                Location=location,
                StartTime=startTime,
                EventType=eventType,
                DurationInHours=durationInHours,
                Description=description,
                OtherDetails=otherDetails,
                InviteByEmail=inviteByEmail,
                BookId=bookId,
                BookUser=user,
                Comment=comment
            };

        }
    }
}
