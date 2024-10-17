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

namespace Hotel_Reservation_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
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

            if (user == null)
            {
                return NotFound("User not found.");
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
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Users'  is null.");
            }
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserByEmailAndPassword), new { email = user.Email, password = user.Password }, user);
        }


        /* public async Task<ActionResult<IEnumerable<User>>> GetUsers()
          {
              if (_context.Users == null)
              {
                  return NotFound();
              }
              return await _context.Users.ToListAsync();
          }*/





    }
}
