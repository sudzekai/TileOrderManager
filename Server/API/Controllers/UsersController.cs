using BLL.DTO.Objects.User.Create;
using BLL.DTO.Objects.User.Special;
using BLL.DTO.Objects.User.Update;
using BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? page)
        {
            try
            {
                var users = await _service.GetUsersAsync();

                if (page.HasValue)
                {
                    var pagedUsers = users.Skip((page.Value - 1) * 5)
                                          .Take(5)
                                          .ToList();
                    return Ok(pagedUsers);
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserFull(long id)
        {
            try
            {
                var user = await _service.GetByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/info")]
        public async Task<IActionResult> GetUserInfo(long id)
        {
            try
            {
                var user = await _service.GetInfoByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/chat-info")]
        public async Task<IActionResult> GetUserChatInfo(long id)
        {
            try
            {
                var user = await _service.GetUserChatInfoByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> PostUser(UserCreateDto user)
        {
            try
            {
                var createdUser = await _service.CreateUserAsync(user);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> PutUser(long id, UserUpdateDto user)
        {
            try
            {
                var result = await _service.UpdateUserAsync(id, user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/info/update")]
        public async Task<IActionResult> PutUserInfo(long id, UserInfoDto user)
        {
            try
            {
                var result = await _service.UpdateUserAsync(id, user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/chat-info/update")]
        public async Task<IActionResult> PutUserChatInfo(long id, UserChatInfoDto user)
        {
            try
            {
                var result = await _service.UpdateUserAsync(id, user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/delete")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                var result = await _service.DeleteUserAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}