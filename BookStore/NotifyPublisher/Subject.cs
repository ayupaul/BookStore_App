using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.NotifyPublisher
{
    public class Subject : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(CreateBookViewModel bookData, CommentViewModel commentData)
        {
            foreach(var observer in _observers)
            {
                observer.Update(bookData, commentData);
            }
        }
    }
}
