using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hotel_Reservation_System.Data;
using Hotel_Reservation_System.Models;

namespace Hotel_Reservation_System.Controllers
{
    
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
        [Route("api/[controller]")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
         {
             if (_context.Users == null)
             {
                 return NotFound();
             }
             return await _context.Users.ToListAsync();
         }


    }
}
