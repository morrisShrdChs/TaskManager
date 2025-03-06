using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

namespace TaskManager.Data
{
    public class TaskManagerDbContext : IdentityDbContext<User>
    { 
        public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) { }
        public DbSet<Tasks> Tasks { get; set; }

    }
}
