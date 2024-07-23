using BookStore.NotifyHub;
using BookStore.NotifyPublisher;
using BuisnessLayer.Data;
using BuisnessLayer.Data.Repository.IServices;
using BuisnessLayer.Facade;
using BuisnessLayer.UnitOfWork;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class CommentController : Controller
    {
        //private readonly AppDbContext context;
        //private readonly IUnitOfWork unitOfWork;
        private readonly IBookReadingEventFacade bookReadingEventFacade;

        //private readonly IBookEventService bookEventService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IHubContext<NotificationHub> hubContext;
     

        //private readonly ICommentService commentService;

        public CommentController(/*AppDbContext context,*/ /*IBookEventService bookEventServiceIUnitOfWork unitOfWork*/IBookReadingEventFacade bookReadingEventFacade,UserManager<IdentityUser> userManager,IHubContext<NotificationHub> hubContext/*ICommentService commentService*/)
        {
            //this.context = context;
            //this.unitOfWork = unitOfWork;
            this.bookReadingEventFacade = bookReadingEventFacade;
            this.userManager = userManager;
            this.hubContext = hubContext;
          
            //this.commentService = commentService;
        }
        [HttpGet]
        public IActionResult AddComment()
        {
            //var findBook = bookEventService.ViewBook(Id);
            //var comment = new CommentViewModel
            //{
            //    BookView = findBook
            //};
            return View();
        }
        [HttpPost]
        public IActionResult AddComment(CommentViewModel model)
        {
            CreateBookViewModel data = JsonConvert.DeserializeObject<CreateBookViewModel>(TempData["MyData"].ToString());
            //model.BookView = context.CreateBooks.Find(data.Id);
            model.BookView = bookReadingEventFacade.ViewBook(data.Id);
            //var user = await userManager.GetUserAsync(User);
            //model.User.Id = user.Id;
            //context.Comments.Add(model);
            //context.SaveChanges();
            bookReadingEventFacade.AddCommentToParticularBook(model);
            var publisherId = model.BookView.BookId;
            var publisherObserver = new Observer(publisherId,hubContext);
            var bookSubject = new Subject();
            bookSubject.Attach(publisherObserver);
            bookSubject.Notify(model.BookView, model);
            return RedirectToAction("ViewBook", "Book", new { id = data.Id });



        }
    }
}
