using Microsoft.AspNetCore.Mvc;
using Service.UserServices;
using UserProfileData.DTO;
using UserProfileServices.UserServices;

namespace UserManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEmailService _emailService; 

        public UserProfileController(IUserService userService, IEmailService emailService)
        {
            _userService = userService;
            _emailService = emailService; 
        }

        [HttpPost("create/user")]
        public async Task<IActionResult> CreateUserProfile(UserProfileDto userProfile)
        {
            if (ModelState.IsValid)
            {
                var createUser = await _userService.CreateUserProfile(userProfile);
                if (createUser.StatusCode == 200)
                {
                    // Send registration confirmation email
                    string subject = "Welcome to Infinion Coding Challenge!";
                    string body = $"Dear {userProfile.FirstName}, thank you for registering with us.";

                    await _emailService.SendEmailAsync(userProfile.Email, subject, body);

                    return Ok(createUser);
                }
                return BadRequest(createUser);
            }
            return BadRequest(userProfile);
        }

        [HttpPost("authenticate/user")]

        public async Task<IActionResult> AuthenticateUser(LoginRequestDto userProfile)
        {
            if(ModelState.IsValid)
            {
                var createUser = await _userService.AuthenticateUser(userProfile);
                if(createUser.StatusCode == 200)
                {
                    return Ok(createUser);
                }
                return BadRequest(createUser);
            }
            return BadRequest(userProfile);
        }
        [HttpGet("user/profile")]
        public async Task<IActionResult> GetUserProfile(string token)
        {
            if(ModelState.IsValid)
            {

                var userProfile = await _userService.GetUserProfile(token);
                if(userProfile.StatusCode == 200)
                {
                    return Ok(userProfile);
                }
                return BadRequest(userProfile);
            }
            return BadRequest(token);
        }
        [HttpPut("update/user/profile")]
        public async Task<IActionResult> UpdateUserProfile(string token, UserProfileUpdateDto userProfileUpdateDto)
        {
            if(ModelState.IsValid)
            {
                var userProfile = await _userService.UpdateUserProfile(token, userProfileUpdateDto);
                if(userProfile.StatusCode == 200)
                {
                    return Ok(userProfile);
                }
                return BadRequest(userProfile);
            }
            return BadRequest(userProfileUpdateDto);
        }
    }
}
