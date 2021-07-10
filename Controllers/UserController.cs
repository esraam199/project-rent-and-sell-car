using CarsApi.Models;
using CarsApi.Services.Interface;
using CarsApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUser _db;
        private IMail _mailService;

        public UserController(IUser db, IMail mailService)
        {
            _db = db;
            _mailService = mailService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> RegisterAsync(RegisterViewModel register)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.RegisterAsync(register);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.LoginUserAsync(login);

                if (result.IsSuccess)
                {
                    await _mailService.SendEmilAsync(login.Email, "Confirm Email", "<h1>This is Confirmation</h1><p>New login at " + DateTime.Now + "</p>");
                    return Ok(result);
                }
                else
                {
                    return BadRequest("Invalid operation");
                }
            }

            return BadRequest("fields are not valid");
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _db.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Ok("Confirmed!!");
            }

            return BadRequest(result);
        }

        [HttpPost("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return NotFound();

            var result = await _db.ForgetPasswordAsync(email);

            if (result.IsSuccess == true)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("ResetPassword")]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel reset)
        {
            if (ModelState.IsValid)
            {
                var result = await _db.ResetPasswordAsync(reset);

                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest();
            }
            return BadRequest();
        }

        [HttpGet("Profile/{email}")]
        [Authorize]
        public async Task<ActionResult> GetUserProfileData([FromRoute]string email)
        {
            var profile = await _db.GetProfileData(email);
            if (profile.IsSuccess == true)
                return Ok(profile);

            return BadRequest();
        }

        [HttpPost("OwingCar")]
        [Authorize]
        public async Task<ActionResult> OwingUserCar([FromBody] int carDetailsId)
        {
            var id = User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier).Value.ToString();
            var result = await _db.OwingCar(id, carDetailsId);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("UserCars/{email}")]
        [Authorize]
        public async Task<ActionResult> GetUserCars(string email)
        {
            var cars = await _db.GetAllUserCars(email);
            if (cars.IsSuccess)
                return Ok(cars.Cars);

            return BadRequest();
        }


        [HttpDelete("{userCarId}")]
        [Authorize]
        public async Task<ActionResult> DeleteUserCar([FromRoute]int userCarId)
        {
            var id = User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier).Value.ToString();
            var result = await _db.DeleteOwnedCar(id, userCarId);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("UserEmail")]
        [Authorize]
        public async Task<ActionResult> GetLoginEmail()
        {
            var id = User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier).Value.ToString();
            var result = await _db.GetUserEmail(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("SameUser/{email}")]
        public async Task<ActionResult> IsSame(string email)
        {
            var id = User.Claims.FirstOrDefault(f => f.Type == ClaimTypes.NameIdentifier).Value.ToString();
            var result = await _db.IsSameUser(id, email);

            if (result.IsSuccess)
                return Ok(result);


            return BadRequest(result);
        }

    }
}
