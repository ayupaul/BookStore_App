using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

 

namespace DataAccessLayer.Models
{
    public class CreateBookViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Event Type")]
        public string EventType { get; set; }
        [Display(Name = "Duration In Hours")]
        public int DurationInHours { get; set; }
        public string Description { get; set; }
        [Display(Name = "Other Details")]
        public string OtherDetails { get; set; }
        [Display(Name = "Invited By Email")]
        public string InviteByEmail { get; set; }
        [Required]
        public string BookId { get; set; }
        [ForeignKey("BookId")]
        public ApplicationUser BookUser { get; set; }
        public ICollection<CommentViewModel> Comment { get; set; } = new List<CommentViewModel>();
    }
}
