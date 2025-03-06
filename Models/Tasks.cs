using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Tasks
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset ExpDate { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public int StatusId { get; set; }
        [Required]
        public User User { get; set; }
    }
}
