using System.Net.WebSockets;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Models;

namespace Task_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User updateUser)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Email = updateUser.Email;
            user.UserName = updateUser.UserName;

            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser (string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if(!result.Succeeded)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Нет данных о пользователе");
            }

            var user = new User
            {
                UserName = newUser.UserName,
                Email = newUser.Email,
                Name = newUser.Name,
                Role = newUser.Role,
                PasswordHash = newUser.PasswordHash
            };



            var result = await _userManager.CreateAsync(user, newUser.PasswordHash);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(RegisterUser), new { id = user.Id }, user);
        }
    }
}
