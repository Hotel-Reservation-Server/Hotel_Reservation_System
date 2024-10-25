using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Reservation_System.Data;
using Hotel_Reservation_System.Models;
using Hotel_Reservation_System.Dto;
using Hotel_Reservation_System.Password;
using Hotel_Reservation_System.Password_and_Email;
using Hotel_Reservation_System.PasswordHarshing;
using static Hotel_Reservation_System.Controllers.UsersController;

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;
        private readonly IPasswordHasher _passwordHasher;

        public UsersController(UserDbContext context, IPasswordHasher passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET: Users


        [HttpGet]
        public IQueryable<UserDto> GetUserdto()
        {
            var user = from b in _context.Users
                       select new UserDto()
                       {
                           FirstName = b.FirstName,
                           LastName = b.LastName,
                           Email = b.Email,
                           NIN = b.NIN,
                           Gender = b.Gender,
                           PhoneNo = b.PhoneNo,
                           StateofResidence = b.StateofResidence
                       };

            return user;
        }

        [HttpGet("GetUserByEmailAndPassword")]
        public async Task<ActionResult<UserDto>> GetUserByEmailAndPassword(string email, string password)
        {
            // Find user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            var result = _passwordHasher.verify(user.Password, password);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            if (!result)
            {
                throw new Exception("Username or Password is not correct");
            }
            // Map to DTO (to avoid exposing sensitive info)
            var userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                NIN = user.NIN,
                Gender = user.Gender,
                PhoneNo = user.PhoneNo,
                StateofResidence = user.StateofResidence
                // Do not return sensitive information like the password
            };

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(User user)
        {
            var passwordHasherstring = _passwordHasher.Hash(user.Password);
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Users'  is null.");
            }

            PasswordValidator passwordvalidator = new PasswordValidator();
            EmailValidation emailValidation = new EmailValidation();
            // Validate the password strength
            if (!passwordvalidator.IsPasswordStrong(user.Password))
            {
                return BadRequest("Password must be at least 8 characters long, and contain an uppercase letter, a lowercase letter, a number, and a special character.");
            }
            // Validate Email
            if (!emailValidation.IsValidEmail(user.Email))
            {
                return BadRequest("Not a valid Email address");
            }
            var newuser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = passwordHasherstring,
                Email = user.Email,
                NIN = user.NIN,
                Gender = user.Gender,
                PhoneNo = user.PhoneNo,
                StateofResidence = user.StateofResidence
            };
            _context.Users.Add(newuser);
            await _context.SaveChangesAsync();

            // Map to DTO
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                NIN = user.NIN,
                Gender = user.Gender,
                PhoneNo = user.PhoneNo,
                StateofResidence = user.StateofResidence
            };
        }

       /* [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            PasswordValidator passwordvalidator = new PasswordValidator();
            EmailValidation emailValidation = new EmailValidation();
            // Find the user by their email
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == updateUserDto.Email);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            if (!_passwordHasher.verify(updateUserDto.Password, user.Password))
            {
                return Unauthorized("Invalid email or password.");
            }

            //update user fields
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Gender = updateUserDto.Gender;
            user.PhoneNo = updateUserDto.PhoneNo;
            user.StateofResidence = updateUserDto.StateofResidence;
            if (!string.IsNullOrWhiteSpace(updateUserDto.Email) && updateUserDto.Email != user.Email)
            {
                // Validate the new email format using your custom method
                if (!emailValidation.IsValidEmail(updateUserDto.Email))
                {
                    return BadRequest("Invalid email format.");
                }

                // Ensure the new email is unique (you might want to check if it already exists in the database)
                var existingUser = await _context.Users.SingleOrDefaultAsync(u => u.Email == updateUserDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("Email is already in use.");
                }

                // Update the email
                user.Email = updateUserDto.Email;
            }

            if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                // Validate the new password using your custom method
                if (!passwordvalidator.IsPasswordStrong(updateUserDto.Password))
                {
                    return BadRequest("New password does not meet the security requirements.");
                }

                // Hash the new password using your custom hashing method
                user.Password = _passwordHasher.Hash(updateUserDto.Password);
            }

            // Save the updated user data in the database
            await _context.SaveChangesAsync();

            return Ok("User data updated successfully.");
        }

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] DeleteUserDto deleteUserDto)
        {
            // Retrieve the user data from the database based on the provided email and password
            
            var user = _context.Users.FirstOrDefault(u => u.Email == deleteUserDto.Email);
            var result = _passwordHasher.verify(user.Password, deleteUserDto.Password);

            if (user == null)
            {
                return NotFound();
            }
            if (!result)
            {
                throw new Exception("Username or Password is not correct");
            }
            // Remove the user data from the database
            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }*/
    }
}
