using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.Models
{
    public class User : IdentityUser
    {
        public User ()
        {
            Tasks = new List<Tasks>();
        }
        public ICollection<Tasks> Tasks { get; set; }


    }
}
