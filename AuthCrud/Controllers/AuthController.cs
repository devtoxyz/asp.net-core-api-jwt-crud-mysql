using AuthCrud.Models;
using AuthCrud.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AuthCrud.Controllers
{
    public class LoginParameters
    {
        public required string email { get; set; }
        public required string password { get; set; }
    }
    public class RegisterParameters
    {
        public required string email { get; set; }
        public required string password { get; set; }
        public required string name { get; set; }
    }

    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        ModelContext context = new ModelContext();

        // POST api/login
        [HttpPost("login")]
        public IActionResult login(LoginParameters parameters)
        {
            User? user = context.Users.FirstOrDefault(u => u.email == parameters.email);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            PasswordHasher<User> hasher = new PasswordHasher<User>();
            PasswordVerificationResult verificationResult = hasher.VerifyHashedPassword(user, user.password, parameters.password);
            if (verificationResult.ToString() == "Failed")
            {
                return NotFound(new { message = "Invalid Password." });
            }

            AuthService authService = new AuthService();
            string token = authService.GenerateToken(user);
            return Ok(new { token });
        }

        // POST api/register
        [HttpPost("register")]
        public IActionResult register(RegisterParameters parameters)
        {
            try
            {
                User user = new User { email = parameters.email, password = parameters.password, name = parameters.name };

                var results = new List<ValidationResult>();
                var validContext = new ValidationContext(user, null, null);
                bool isValid = Validator.TryValidateObject(user, validContext, results, true);

                if (!isValid)
                {
                    return NotFound(new { message = "Error!" });
                }

                PasswordHasher<User> hasher = new PasswordHasher<User>();
                string hashedPassword = hasher.HashPassword(user, user.password);
                user.password = hashedPassword;

                context.Users.Add(user);
                context.SaveChanges();

                return Ok(new { message = "Register Success!" });
            }
            catch (Exception e)
            {
                return NotFound(new { message = "Error!" });
            }
        }
    }
}
