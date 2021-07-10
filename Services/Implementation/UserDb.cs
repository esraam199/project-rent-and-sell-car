using CarsApi.Models;
using CarsApi.Services.Interface;
using CarsApi.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace CarsApi.Services.Implementation
{
    public class UserDb : IUser
    {
        private UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;
        private IMail _mail;
        private CarsContext _db;

        public UserDb(UserManager<ApplicationUser> userManager, IConfiguration configuration, IMail mail, CarsContext db)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mail = mail;
            _db = db;
        }
        public async Task<UserProfileViewModel> GetProfileData(string email)
        {
            UserProfileViewModel userProfileData = new UserProfileViewModel();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new UserProfileViewModel { IsSuccess = false };

            var phone = _db.UserPhone.FirstOrDefault(w => w.UserId == user.Id);
            

            userProfileData.IsSuccess = true;
            userProfileData.Fullname = user.FullName;
            userProfileData.Email = user.Email;
            if(phone != null)
                userProfileData.PhoneNo = phone.Number;

            return userProfileData;
        }
        public async Task<MessageResponseViewModel> RegisterAsync(RegisterViewModel registeration)
        {
            if (registeration == null)
                throw new NullReferenceException("User is null");

            var identityUser = new ApplicationUser
            {
                Email = registeration.Email,
                UserName = registeration.Email,
                FullName = registeration.FullName,
                NationalId = registeration.NationalId,
                PersonalLicenceNo = registeration.PersonalLicenceNo
            };

            var result = await _userManager.CreateAsync(identityUser, registeration.Password);
            UserPhone phone = new UserPhone
            {
                Number = registeration.PhoneNo,
                UserId = identityUser.Id
            };
            _db.UserPhone.Add(phone);
            _db.SaveChanges();


            if (result.Succeeded)
                return new MessageResponseViewModel
                {
                    Message = "account created successfully",
                    IsSuccess = true,
                };


            return new MessageResponseViewModel
            {
                Message = "something went wrong while creating account",
                IsSuccess = false,
                Errors = result.Errors.Select(s => s.Description)
            };
        }

        public async Task<MessageResponseViewModel> LoginUserAsync([FromBody] LoginViewModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                return new MessageResponseViewModel
                {
                    Message = "there is no account attached to that email",
                    IsSuccess = false
                };
            }

            bool result = await _userManager.CheckPasswordAsync(user, login.Password);

            if (!result)
            {
                return new MessageResponseViewModel
                {
                    Message = "Invalid Credenitials",
                    IsSuccess = false
                };
            }

            var Claims = new[]
            {
                new Claim("Email",login.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(_configuration["AuthSettings:Issuer"], _configuration["AuthSettings:Issuer"], claims: Claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new MessageResponseViewModel
            {
                Token = tokenString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                Fullname = user.FullName,
                Email = user.Email
            };
        }

        public async Task<MessageResponseViewModel> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "Email Confirmed Successfully"
                };


            return new MessageResponseViewModel
            {
                IsSuccess = false,
                Message = "Email not confirmed",
                Errors = result.Errors.Select(s => s.Description)
            };

        }

        public async Task<MessageResponseViewModel> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "User not found"
                };

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedChangePaaswordToken = Encoding.UTF8.GetBytes(token);
            var validChangePasswordToken = WebEncoders.Base64UrlEncode(encodedChangePaaswordToken);

            string url = $"{_configuration["AppUrl"]}api/user/resetpassword?email={email}&token={validChangePasswordToken}";

            await _mail.SendEmilAsync(email, "Reset Password", $"<h1>Reset Your Password</h1> <p>to resest your password</p><a href='{url}' >click here</a>");

            return new MessageResponseViewModel
            {
                IsSuccess = true,
                Message = "reset password url has been sent to your email",
            };

        }

        public async Task<MessageResponseViewModel> ResetPasswordAsync(ResetPasswordViewModel reset)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(reset.Email);
            if (user == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(reset.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ResetPasswordAsync(user, normalToken, reset.Password);

            if (result.Succeeded)
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "password changes successfully"
                };

            return new MessageResponseViewModel
            {
                Message = "something went wrong",
                IsSuccess = false,
                Errors = result.Errors.Select(s => s.Description)
            };
        }

        public async Task<MessageResponseViewModel> OwingCar(string userId, int carDetailsId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "User Not Found"
                };

            var car = _db.CarDetails
                .Include(i => i.ModelClass)
                .FirstOrDefault(f => f.Id == carDetailsId);

            if (car == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "Car Not Found"
                };

            try
            {
                CarDetails details = new CarDetails
                {
                    IsFromSystem = false,
                    ModelClassId = car.ModelClassId
                };

                _db.CarDetails.Add(details);
                await _db.SaveChangesAsync();

                UserCar userCar = new UserCar
                {
                    UserId = userId,
                    CarDetailsId = details.Id
                };

                _db.UserCar.Add(userCar);
                await _db.SaveChangesAsync();

                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "Car Added To User successfully"
                };
            }
            catch (Exception)
            {
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                };

            }

        }

        public async Task<UserCars> GetAllUserCars(string email)
        {
            UserCars userCars = new UserCars();

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return new UserCars
                {
                    IsSuccess = false
                };

            var carsOfUser = _db.UserCar
                .Include(i => i.CarDetails)
                .Include(i => i.CarDetails.CarPhotos)
                .Include(i => i.CarDetails.ModelClass)
                .Include(i => i.CarDetails.ModelClass.Model)
                .Include(i => i.CarDetails.ModelClass.Model.Brand)
                .Where(w => w.UserId == user.Id && w.IsDeleted == false)
                .ToList();

            foreach (var oneCar in carsOfUser)
            {
                string photoName = oneCar.CarDetails.CarPhotos.Select(s => s.PhotoName).FirstOrDefault();
                if (photoName == null)
                {
                    photoName = _db.CarPhoto
                        .Include(i => i.CarDetails)
                        .Include(i => i.CarDetails.ModelClass)
                        .Where(w => w.CarDetails.ModelClass.Id == oneCar.CarDetails.ModelClass.Id && w.CarDetails.IsFromSystem == true)
                        .Select(s => s.PhotoName).FirstOrDefault();
                }

                ChooseCarViewModel car = new ChooseCarViewModel
                {
                    CarDetailsId = oneCar.CarDetails.Id,
                    CarName = oneCar.CarDetails.ModelClass.Model.Brand.Name + " " + oneCar.CarDetails.ModelClass.Model.Name + " " + oneCar.CarDetails.ModelClass.Model.Year,
                    ClassName = oneCar.CarDetails.ModelClass.ClassName,
                    ImgName = photoName,
                    UserCarId = oneCar.Id
                };
                userCars.Cars.Add(car);
            }

            userCars.IsSuccess = true;
            return userCars;
        }

        public async Task<MessageResponseViewModel> DeleteOwnedCar(string userId, int userCarId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "User not found"
                };

            var UserCar = _db.UserCar.FirstOrDefault(f => f.UserId == userId && f.Id == userCarId);
            if (UserCar == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "can not find your car"
                };

            try
            {
                UserCar.IsDeleted = true;
                _db.SaveChanges();
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "Deleted Successfully"
                };
            }
            catch (Exception)
            {

                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "Something went wrong"
                };
            }


        }


        private async Task<string> GenerateConfirmationTokenURL(ApplicationUser identityUser)
        {
            var confirmEmailToken = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            return $"{_configuration["AppUrl"]}api/user/ConfirmEmail?userId={identityUser.Id}&token={validEmailToken}";
        }

        public async Task<MessageResponseViewModel> GetUserEmail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "user not found"
                };

            return new MessageResponseViewModel
            {
                IsSuccess = true,
                Message = user.Email
            };
        }

        public async Task<MessageResponseViewModel> IsSameUser(string userId, string email)
        {
            if (userId == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "different"
                };

            var currentLoginUser = await _userManager.FindByIdAsync(userId);

            if (currentLoginUser == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "user not found"
                };
            var userProfile = await _userManager.FindByEmailAsync(email);

            if (userProfile == null)
                return new MessageResponseViewModel
                {
                    IsSuccess = false,
                    Message = "user not found"
                };

            if (currentLoginUser.Id == userProfile.Id)
                return new MessageResponseViewModel
                {
                    IsSuccess = true,
                    Message = "same"
                };

            return new MessageResponseViewModel
            {
                IsSuccess = true,
                Message = "different"
            };

        }
    }
}
