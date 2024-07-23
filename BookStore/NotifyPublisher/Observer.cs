using BookStore.NotifyHub;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.NotifyPublisher
{
    public class Observer : IObserver
    {
        private readonly string _publisherId;
        private readonly IHubContext<NotificationHub> hubContext;

        public Observer(string publisherId,IHubContext<NotificationHub> hubContext)
        {
            _publisherId = publisherId;
            this.hubContext = hubContext;
        }
        public async void Update(CreateBookViewModel bookData, CommentViewModel commentData)
        {
            var message = $"A new Commented was added to your book:{bookData.Title} and the commment is:{commentData.CommentText}";
            await hubContext.Clients.User(_publisherId).SendAsync("RecieveMssg", "New Comment Arrived", message);
        }

       
    }
}
