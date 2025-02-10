using IdentityApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using IdentityApi.DTO;


namespace IdentityApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class RegController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public RegController(UserManager<User> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userRegistration)
        {
            if(userRegistration == null)
            {
                return BadRequest();
            }
            var user = _mapper.Map<User>(userRegistration);
            var result = await _userManager.CreateAsync(user,userRegistration.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e=>e.Description).ToList();
                return BadRequest(new RegResponseDto {Errors = errors });
            }
            return Ok(new { Message = "User registered successfully!" });

        }
            
    }
}
