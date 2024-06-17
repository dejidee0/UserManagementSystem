using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserProfileServices.UserServices;

namespace UserManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailDto emailDto)
        {
            if (string.IsNullOrEmpty(emailDto.ToEmail))
            {
                return BadRequest("Email address is required.");
            }

            try
            {
                await _emailService.SendEmailAsync(emailDto.ToEmail, "Test Email", "This is a test email.");
                return Ok("Email sent successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    public class EmailDto
    {
        public string ToEmail { get; set; }
    }
}
