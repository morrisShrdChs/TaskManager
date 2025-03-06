using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpDate { get; set; }

        public IdentityUser User { get; set; }

    }
}
