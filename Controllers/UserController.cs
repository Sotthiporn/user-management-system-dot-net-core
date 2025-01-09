using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using user_management_dot_net_core.Dtos.Request;
using user_management_dot_net_core.Exceptions;
using user_management_dot_net_core.Services.Interfaces;

namespace user_management_dot_net_core.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public partial class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _service.GetAllAsync();
                return Ok(new
                {
                    Success = true,
                    Message = "Users retrieved successfully.",
                    Data = response
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var response = await _service.GetByIdAsync(id);
                return Ok(new
                {
                    Success = true,
                    Message = "User retrieved successfully.",
                    Data = response
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateProfileRequest profileRequest)
        {
            try
            {
                await _service.UpdateAsync(id, profileRequest);
                return Ok(new { Success = true, Message = "User updated successfully" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new { Success = true, Message = "User deleted successfully" });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { Success = false, Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An unexpected error occurred.", Details = ex.Message });
            }
        }
    }
}
