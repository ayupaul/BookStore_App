using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
   public class ViewBookViewModel
    {
        public CreateBookViewModel Book { get; set; }
        public List<CommentViewModel> CommentList { get; set; }
    }
}
