using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class CommentViewModel
    {
        public int  Id { get; set; }
        public string CommentText { get; set; }
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int BookEventId { get; set; }
        public CreateBookViewModel  BookView{ get; set; }
    }
}
