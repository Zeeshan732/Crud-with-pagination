using Crud_with_pagination.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud_with_pagination.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly UserContext _context;
        public UserController(IConfiguration config, UserContext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("CreateUser")]
        public IActionResult Create(User user)
        {
            if (_context.Users.Where(u => u.Email == user.Email).FirstOrDefault() != null)
            {
                return Ok("Already Exist");
            }
             _context.Users.Add(user);
            _context.SaveChanges();
            return Ok("Success");
        }

        [HttpPost("LoginUser")]
        public IActionResult Login(LoginUser user)
        {
            var available = _context.Users.Where(u => u.Email == user.Email && u.Password == user.Password).FirstOrDefault();

            if (available != null) {
                return Ok("Success");
            }

            return Ok("Failure");

        }

    }
}
