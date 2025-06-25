using Microsoft.AspNetCore.Mvc;
using projectManagementSystem.Domain.Entities;
using projectManagementSystem.Repositories.ProjectsTasks;
using projectManagementSystem.Repositories.Users;

namespace projectManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepositories _userRepositories;

        public UserController(IUserRepositories userRepositories)
        {
            _userRepositories = userRepositories;
        }

        [HttpPost]
        [Route(nameof(AddUser))]
        public async Task<User> AddUser(User user, CancellationToken cancellationToken)
        {
            await _userRepositories.AddUser(user, cancellationToken);
            return user;
        }

        [HttpGet]
        [Route(nameof(GetUserByLogin))]
        public async Task<User> GetUserByLogin([FromQuery] string login, CancellationToken cancellationToken)
        {
            return await _userRepositories.GetUserByLogin(login, cancellationToken);
        }

        [HttpGet]
        [Route(nameof(GetUserByName))]
        public async Task<User> GetUserByName([FromQuery] string name, CancellationToken cancellationToken)
        {
            return await _userRepositories.GetUserByName(name, cancellationToken);
        }

        [HttpDelete]
        [Route(nameof(DeleteUser))]
        public async Task<IActionResult> DeleteUser([FromQuery] string userName, CancellationToken cancellationToken)
        {
            await _userRepositories.DeleteUser(userName, cancellationToken);
            return Ok();
        }

        [HttpPut]
        [Route(nameof(UpdateUser))]
        public async Task<IActionResult> UpdateUser([FromQuery] int id, [FromQuery] string name, [FromQuery] string role, CancellationToken cancellationToken)
        {
            await _userRepositories.UpdateUser(id, name, role, cancellationToken);
            return Ok();
        }

        [HttpPatch]
        [Route(nameof(UpdateLoginPasswordById))]
        public async Task<IActionResult> UpdateLoginPasswordById([FromQuery] int id, [FromQuery] string login, [FromQuery] string password, CancellationToken cancellationToken)
        {
            await _userRepositories.UpdateLoginPasswordById(id, login, password, cancellationToken);
            return Ok();
        }
    }
}
