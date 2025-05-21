using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;
using TaskManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TaskController : ControllerBase
    {
        private readonly TaskManagerDbContext _context;
        private readonly UserManager<User> _userManager;

        public TaskController(TaskManagerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllTasks()
        {
            var username = User.Identity?.Name;
            Console.WriteLine($"GetAllTasks для пользователя: {username}");
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var tasks = await _context.Tasks
                .Include(t => t.User)
                .Where(t => t.User.UserName == username)
                .ToListAsync();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Tasks task)
        {
            if (task == null)
            {
                return BadRequest("Данные о задаче отсутствуют");
            }

            if (!string.IsNullOrEmpty(task.UserId))
            {
                var user = await _userManager.FindByIdAsync(task.UserId);
                if (user == null)
                {
                    return BadRequest("Пользователь не найден");
                }
            }
            else
            {
                task.UserId = null;
            }

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] Tasks updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.Name = updatedTask.Name;
            task.Description = updatedTask.Description;
            task.StartDate = updatedTask.StartDate;
            task.ExpDate = updatedTask.ExpDate;
            task.StatusId = updatedTask.StatusId;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
