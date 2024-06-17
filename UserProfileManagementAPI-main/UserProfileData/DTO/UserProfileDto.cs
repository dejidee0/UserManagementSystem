using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UserProfileData.DTO
{
    public class UserProfileDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }  
        [Required]
        public string LastName { get; set; }   
    }

}
