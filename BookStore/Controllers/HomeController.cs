using BuisnessLayer.Data;
using BuisnessLayer.Data.Repository.IServices;
using BuisnessLayer.Facade;
using BuisnessLayer.UnitOfWork;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookReadingEventFacade bookReadingEventFacade;
        //private readonly IUnitOfWork unitOfWork;
        //private readonly IBookEventService bookEventService;
       

        public HomeController(ILogger<HomeController> logger,/*IBookEventService bookEventService IUnitOfWork unitOfWork*/IBookReadingEventFacade bookReadingEventFacade)
        {
            _logger = logger;
            this.bookReadingEventFacade = bookReadingEventFacade;
            //this.unitOfWork = unitOfWork;
            //this.bookEventService = bookEventService;
           
        }

        public IActionResult Index()
        {
            var books = bookReadingEventFacade.GetAllBooks();
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
