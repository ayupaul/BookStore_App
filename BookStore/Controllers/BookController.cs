using System.Security.Claims;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLayer.Facade;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using BuisnessLayer.Data;
using BuisnessLayer.Data.Repository.IServices;
using Newtonsoft.Json;
using BuisnessLayer.Repository;
using BuisnessLayer.UnitOfWork;
using BuisnessLayer;
using BuisnessLayer.Exceptions;

namespace BookStore.Controllers

{
    [Authorize]
    public class BookController : Controller

    {
        //private readonly IUnitOfWork unitOfWork;
        private readonly IBookReadingEventFacade bookReadingEventFacade;


        //private readonly IunitOfWork.bookEventService unitOfWork.bookEventService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IBookReadingEventFactory bookReadingEventFactory;


        //private readonly AppDbContext context;

        public BookController(/*IunitOfWork.bookEventService unitOfWork.bookEventServiceIUnitOfWork unitOfWork*/IBookReadingEventFacade bookReadingEventFacade, UserManager<IdentityUser> userManager,IBookReadingEventFactory bookReadingEventFactory)

        {
            //this.unitOfWork = unitOfWork;
            this.bookReadingEventFacade = bookReadingEventFacade;


            //this.unitOfWork.bookEventService = unitOfWork.bookEventService;

            this.userManager = userManager;
            this.bookReadingEventFactory = bookReadingEventFactory;
        }



        [HttpGet]


        public IActionResult CreateBook()

        {

            return View();

        }

        [HttpPost]

        [CustomExceptionHandler]
        public IActionResult CreateBook(CreateBookViewModel model)

        {
            try
            {
                if (ModelState.IsValid)

                {

                    model.BookId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var book = bookReadingEventFactory.Create(model.Id, model.Title, model.Date, model.Location, model.StartTime, model.EventType, model.DurationInHours, model.Description, model.OtherDetails, model.InviteByEmail, model.BookId, model.BookUser, model.Comment);

                    bookReadingEventFacade.CreateBook(book);

                    return RedirectToAction("Index", "Home");

                }

                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating the book.", ex);
            }
        }
        [AllowAnonymous]
        [CustomExceptionHandler]
        public IActionResult ViewBook(int id)

        {

            // Retrieve the book event with the specified ID from the data source

            CreateBookViewModel book = bookReadingEventFacade.ViewBook(id);
            if (book == null)
            {
                throw new BookNotFoundException($"Book with ID {id} not found.");
            }
            TempData["MyData"] = JsonConvert.SerializeObject(book);
            var userId = userManager.GetUserId(User);
            //var comment = context.Comments.Include(it => it.BookUser).Where(it => it.BookView.Id == id).ToList();
            var comment = bookReadingEventFacade.GetAllCommentsForParticularBook(id);
            var viewModel = new ViewBookViewModel
            {
                Book = book,
                CommentList = comment
            };
            return View(viewModel);

            



          

        }


        public IActionResult MyEvents()

        {

            // Retrieve list of book events created by the logged in user

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<CreateBookViewModel> events = bookReadingEventFacade.MyEvents(userId);

            return View(events);

        }


        [CustomExceptionHandler]
        public IActionResult Edit(int id)

        {

            var (bookReadingEvent, isUpcoming) = bookReadingEventFacade.EditAsync(id);
            if (bookReadingEvent == null)

            {

                throw new BookNotFoundException($"Book with ID {id} not found.");

            }

            if (isUpcoming==true)

            {

                return View(bookReadingEvent);

            }

            return View("ErrorHandler");

        }



        [HttpPost]

        public IActionResult Update(CreateBookViewModel updatedBookReadingEvent)

        {

            var originalBookReadingEvent= bookReadingEventFacade.Update(updatedBookReadingEvent);
            return RedirectToAction("ViewBook", new { id = originalBookReadingEvent.Id });
        }


        public async Task<IActionResult> InvitedTo()

        {

            var userName = User.Identity.Name;

            var user = await userManager.FindByNameAsync(userName);

            var userEmail = user.Email;

            var bookEvents = bookReadingEventFacade.InvitedTo(userEmail);
          return View(bookEvents);

        }


    }

}





