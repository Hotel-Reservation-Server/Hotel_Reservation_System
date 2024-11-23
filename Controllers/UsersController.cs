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
        public IQueryable <GetUserDto> Getuser()
        {
            var getuser = from user in _context.Users
                          select new GetUserDto()
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                NIN = user.NIN,
                Gender = user.Gender,
                PhoneNo = user.PhoneNo,
                StateofResidence = user.StateofResidence
                // Do not return sensitive information like the password
            };

            return getuser;
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
        public IActionResult PostUser(UserDto userDto) 
        {
            var passwordHasherstring = _passwordHasher.Hash(userDto.Password);
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Users'  is null.");
            }

            PasswordValidator passwordvalidator = new PasswordValidator();
            EmailValidation emailValidation = new EmailValidation();
            // Validate the password strength
            if (!passwordvalidator.IsPasswordStrong(userDto.Password))
            {
                return BadRequest("Password must be at least 8 characters long, and contain an uppercase letter, a lowercase letter, a number, and a special character.");
            }
            // Validate Email
            if (!emailValidation.IsValidEmail(userDto.Email))
            {
                return BadRequest("Not a valid Email address");
            }

            var users = new User()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = passwordHasherstring,
                NIN = userDto.NIN,
                Gender = userDto.Gender,
                PhoneNo = userDto.PhoneNo,
                StateofResidence = userDto.StateofResidence
            };
            _context.Users.Add(users);
            _context.SaveChanges();
            var getuser = new GetUserDto
            {
                UserId = users.UserId,
                FirstName = users.FirstName,
                LastName = users.LastName,
                Email = users.Email,
                NIN = users.NIN,
                Gender = users.Gender,
                PhoneNo = users.PhoneNo,
                StateofResidence = users.StateofResidence
            };


            return Ok(getuser);
            
        }
        // Map to DTO
          

       

        [HttpPut("update")]
        public IActionResult UpdateUsers(int userId, UpdateUserDto updateUserDto)
        {
            var passwordHasherstring = _passwordHasher.Hash(updateUserDto.Password);
            PasswordValidator passwordvalidator = new PasswordValidator();
            EmailValidation emailValidation = new EmailValidation();
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Gender = updateUserDto.Gender;
            user.PhoneNo = updateUserDto.PhoneNo;
            user.NIN = updateUserDto.NIN;
            user.StateofResidence = updateUserDto.StateofResidence;
            // Validate the password strength
            if (!passwordvalidator.IsPasswordStrong(updateUserDto.Password))
            {
                return BadRequest("Password must be at least 8 characters long, and contain an uppercase letter, a lowercase letter, a number, and a special character.");
            }
            user.Email = updateUserDto.Email;
            if (!passwordvalidator.IsPasswordStrong(updateUserDto.Password))
            {
                return BadRequest("Password must be at least 8 characters long, and contain an uppercase letter, a lowercase letter, a number, and a special character.");
            }
            user.Password = passwordHasherstring;
            var getuser = new GetUserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                NIN = user.NIN,
                Gender = user.Gender,
                PhoneNo = user.PhoneNo,
                StateofResidence = user.StateofResidence
            };


            return Ok(getuser);
        }
        [HttpDelete]
        public IActionResult DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok();
        }
    }
}
