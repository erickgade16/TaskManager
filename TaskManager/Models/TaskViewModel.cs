using System.ComponentModel.DataAnnotations;
using TaskManager.Attributes;

namespace TaskManager.Models
{
    public class TaskViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title cannot be longer than 100 characters")]
        public  string? Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(300, ErrorMessage = "Description cannot be longer than 300 characters")]
        public  string? Description { get; set; }


        [Required(ErrorMessage = "Due Date is required")]
        [FutureDate(ErrorMessage = "Due date must be greater than today's date")]
        [Display(Name = "Expiration Date")]
        public DateTime DueDate { get; set; }

        [Required(ErrorMessage = "Priority is required")]
        public bool Priority { get; set; }
    }
}
