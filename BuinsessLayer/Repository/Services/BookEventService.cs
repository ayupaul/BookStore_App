using BuisnessLayer.Data;
using BuisnessLayer.Data.Repository.IServices;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Data.Repository.Services
{
    public class BookEventService
    {
   

        private readonly AppDbContext context;

        public BookEventService(AppDbContext context)
        {
          
            this.context = context;
        }
        public void CreateBook(CreateBookViewModel model)
        {
            context.CreateBooks.Add(model);
            context.SaveChanges();

        }

        public  (CreateBookViewModel bookReadingEvent,bool isUpcoming) EditAsync(int id)
        {
            var bookReadingEvent = context.CreateBooks.Find(id);
            var isUpcoming = false;
            if (bookReadingEvent.Date > DateTime.Now)
            {
                isUpcoming = true;
            }
            return (bookReadingEvent,isUpcoming);
            
        }

        public IEnumerable<CreateBookViewModel> GetAllBooks()
        {
            var books = context.CreateBooks.ToList();
            return books;
        }

        public List<CommentViewModel> GetAllCommentsForParticularBook(int id)
        {
            var comment = context.Comments.Include(it => it.User).Where(it => it.BookView.Id == id).ToList();
            return comment;
        }

        public IEnumerable<CreateBookViewModel> InvitedTo(string userEmail)
        {
            var bookEvents =context.CreateBooks

             .Where(be => be.InviteByEmail.Contains(userEmail))

             .ToList();
            return bookEvents;

        }

        public List<CreateBookViewModel> MyEvents(string userId)
        {


            List<CreateBookViewModel> events = context.CreateBooks.Where(e => e.BookId == userId).ToList();

            events.Sort((a, b) => b.StartTime.CompareTo(a.StartTime));
            return events;
        }

        public CreateBookViewModel Update(CreateBookViewModel model)
        {
            var originalBookReadingEvent = context.CreateBooks.Find(model.Id);
            originalBookReadingEvent.Title = model.Title;
            originalBookReadingEvent.Location = model.Location;
            originalBookReadingEvent.InviteByEmail = model.InviteByEmail;
            originalBookReadingEvent.Date = model.Date;
            originalBookReadingEvent.StartTime = model.StartTime;
            originalBookReadingEvent.EventType = model.EventType;
            originalBookReadingEvent.DurationInHours = model.DurationInHours;
            originalBookReadingEvent.Description = model.Description;
            originalBookReadingEvent.OtherDetails = model.OtherDetails;
            context.SaveChanges();
            return originalBookReadingEvent;
        }

        public CreateBookViewModel ViewBook(int id)
        {
            return context.CreateBooks.Find(id);
        }
    }
}
