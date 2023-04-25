using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FixIt.Models
{
    public class ToDoItem
    {
        [Key]
        public Guid ToDoItemId { get; set; }
        public string UserName { get; set; }
        public Boolean IsDone { get; set; }
        [Required]
        [StringLength(50, MinimumLength =3)]
        public string Title { get; set; } = string.Empty;
        public DateTimeOffset? DueAt { get; set; }
    }
}
