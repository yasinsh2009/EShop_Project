using ECommerceApp.Application.Services.Interface;
using ECommerceApp.Domain.DTOs.Account.User;
using ECommerceApp.Domain.Enums.User;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApp.Main.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ValidateUser(UserValidationDto dto)
        {
            var result = await _service.ValidateUser(dto);

            switch (result)
            {
                case UserValidationResult.Active:
                    return Ok(result);
                case UserValidationResult.NotActive:
                    return BadRequest("User is not active.");
                case UserValidationResult.NotFound:
                    return NotFound("User not found.");
                case UserValidationResult.Error:
                    return StatusCode(500, "Error while validation.");
                default:
                    return BadRequest("Uknown status.");
            }
        }
    }
}
