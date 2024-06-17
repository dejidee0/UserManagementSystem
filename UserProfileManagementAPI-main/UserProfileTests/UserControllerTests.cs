using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.UserServices;
using System.Threading.Tasks;
using UserManagementSystemAPI.Controllers;
using UserProfileData.DTO;
using UserProfileServices.UserServices;
using Xunit;

namespace UserProfileTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IEmailService> _emailService;
        private readonly UserProfileController _userProfileController;

        public UserControllerTests()
        {
            _userService = new Mock<IUserService>();
            _emailService = new Mock<IEmailService>();
            _userProfileController = new UserProfileController(_userService.Object, _emailService.Object);
        }

        [Fact]
        public async Task CreateUserProfile_Returns_OkResult()
        {
            // Arrange
            var user = new UserProfileDto
            {
                UserName = "Test",
                Email = "sean@gmail.com",
                Password = "Pass123#",
            };

            _userService.Setup(service => service.CreateUserProfile(user))
                .ReturnsAsync(new ResponseDto<object> { StatusCode = 200 });

            // Act
            var result = await _userProfileController.CreateUserProfile(user);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateUserProfile_With_WrongPasswordFormat_Returns_BadRequest()
        {
            // Arrange
            var user = new UserProfileDto
            {
                UserName = "Test",
                Email = "sean@gmail.com",
                Password = "remmmtis",
            };

            _userService.Setup(service => service.CreateUserProfile(user))
                .ReturnsAsync(new ResponseDto<object> { StatusCode = 400 });

            // Act
            var result = await _userProfileController.CreateUserProfile(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Authenticate_UserProfile_OkRequest()
        {
            // Arrange
            var user = new LoginRequestDto
            {
                UserName = "string",
                Password = "Pass123#",
            };

            _userService.Setup(service => service.AuthenticateUser(user))
                .ReturnsAsync(new APIResponse { StatusCode = 200 });

            // Act
            var result = await _userProfileController.AuthenticateUser(user);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Authenticate_UserProfile_With_WrongPassword_Returns_BadRequest()
        {
            // Arrange
            var user = new LoginRequestDto
            {
                UserName = "string",
                Password = "Pass123567#",
            };

            _userService.Setup(service => service.AuthenticateUser(user))
                .ReturnsAsync(new APIResponse { StatusCode = 404 });

            // Act
            var result = await _userProfileController.AuthenticateUser(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Authenticate_UserProfile_With_WrongUserName_Returns_BadRequest()
        {
            // Arrange
            var user = new LoginRequestDto
            {
                UserName = "string1",
                Password = "Pass123#",
            };

            _userService.Setup(service => service.AuthenticateUser(user))
                .ReturnsAsync(new APIResponse { StatusCode = 404 });

            // Act
            var result = await _userProfileController.AuthenticateUser(user);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Get_UserProfile_With_Token_Returns_OkRequest()
        {
            // Arrange
            var userProfileUpdateDto = new UserProfileUpdateDto();
            string token = "valid_token";

            _userService.Setup(service => service.GetUserProfile(token))
                .ReturnsAsync(new ResponseDto<UserProfileUpdateDto> { StatusCode = 200, Result = userProfileUpdateDto });

            // Act
            var result = await _userProfileController.GetUserProfile(token);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_UserProfile_With_InvalidToken_Returns_BadRequest()
        {
            // Arrange
            string invalidToken = "invalid_token";

            _userService.Setup(service => service.GetUserProfile(invalidToken))
                .ReturnsAsync(new ResponseDto<UserProfileUpdateDto> { StatusCode = 404 });

            // Act
            var result = await _userProfileController.GetUserProfile(invalidToken);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Update_UserProfile_With_ValidToken_Returns_OkRequest()
        {
            // Arrange
            var userProfileUpdateDto = new UserProfileUpdateDto();
            string token = "valid_token";

            _userService.Setup(service => service.UpdateUserProfile(token, userProfileUpdateDto))
                .ReturnsAsync(new ResponseDto<object> { StatusCode = 200, Result = userProfileUpdateDto });

            // Act
            var result = await _userProfileController.UpdateUserProfile(token, userProfileUpdateDto);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Update_UserProfile_With_InvalidToken_Returns_BadRequest()
        {
            // Arrange
            var userProfileUpdateDto = new UserProfileUpdateDto();
            string invalidToken = "invalid_token";

            _userService.Setup(service => service.UpdateUserProfile(invalidToken, userProfileUpdateDto))
                .ReturnsAsync(new ResponseDto<object> { StatusCode = 404 });

            // Act
            var result = await _userProfileController.UpdateUserProfile(invalidToken, userProfileUpdateDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
