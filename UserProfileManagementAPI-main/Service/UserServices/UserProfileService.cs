using Service.UserServices;
using System.Net.Mail;
using UserProfileData.Domain;
using UserProfileData.DTO;
using UserProfileData.Repository;

namespace Service.UserService
{
    public class UserProfileService : IUserService
    {
        private readonly IUserProfileRepo _userProfileRepo;
        public UserProfileService(IUserProfileRepo userProfileRepo)
        {
            _userProfileRepo = userProfileRepo;
        }
        public async Task<ResponseDto<Object>> CreateUserProfile(UserProfileDto userProfile)
        {
            try
            {
                var response = new ResponseDto<Object>();

                var user = new UserProfile
                {
                    Email = userProfile.Email,
                    UserName = userProfile.UserName,
                    FirstName = userProfile.FirstName,
                    LastName = userProfile.LastName
                };
                var createUser = await _userProfileRepo.CreateUserProfile(user, userProfile.Password);
                if (createUser.StatusCode == 200)
                {
                    response.StatusCode = 200;
                    response.DisplayMessage = "Success";
                    response.Result = $"UserProfile with Username {userProfile.UserName} created successfully";
                    return response;
                }
                response.StatusCode = createUser.StatusCode;
                response.DisplayMessage = createUser.DisplayMessage;
                response.Result = createUser.Result;
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<APIResponse> AuthenticateUser(LoginRequestDto userProfile)
        {
            try
            {
                var createUser = await _userProfileRepo.AuthenticateUser(userProfile);
                if (createUser.IsSuccess)
                {
                    return createUser;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ResponseDto<UserProfileUpdateDto>> GetUserProfile(string token)
        {
            try
            {
                var userProfile = await _userProfileRepo.GetLoggedInUserByToken(token);
                if (userProfile != null)
                {
                    return userProfile;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ResponseDto<Object>> UpdateUserProfile(string token, UserProfileUpdateDto userProfileUpdateDto)
        {
            try
            {
                var userProfile = await _userProfileRepo.UpdateUser(token, userProfileUpdateDto);
                if (userProfile != null)
                {
                    return userProfile;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private async Task SendConfirmationEmail(string email, string callbackUrl)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(email));
            message.Subject = "Confirm your email";
            message.Body = $"Please confirm your account by clicking <a href='{callbackUrl}'>here</a>";
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                await smtp.SendMailAsync(message);
            }
        }


    }
}