using Microsoft.AspNetCore.Mvc;
using MovieRentalsAPI.DBContext;
using MovieRentalsAPI.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRentalsAPI.Controllers
{
    public class UserController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        public class UsersController : ControllerBase
        {
            private readonly MovieRentalContext _context;

            public UsersController(MovieRentalContext context)
            {
                _context = context;
            }

            [HttpPost("register")]
            public async Task<IActionResult> Register(User user)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }

            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] User loginUser)
            {
                var user = await _context.Users.SingleOrDefaultAsync(u => u.EmailAddress == loginUser.EmailAddress && u.Password == loginUser.Password);

                if (user == null)
                    return Unauthorized();

                return Ok(user);
            }

            
        }
    }
}
