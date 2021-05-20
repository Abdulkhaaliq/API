
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiddlewareAPI.DataContext;
using MiddlewareAPI.Models;
using MiddlewareAPI.Services.Interfaces;

namespace MiddlewareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RPAdatabaseContext _context;
        private readonly IIdentityService _identityService;

        public UsersController(RPAdatabaseContext context, IIdentityService identityService)
        {
            _context = context;
            _identityService = identityService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTable>>> GetUserTables()
        {
            return await _context.UserTables.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTable>> GetUserTable(int id)
        {
            var userTable = await _context.UserTables.FindAsync(id);

            if (userTable == null)
            {
                return NotFound();
            }

            return userTable;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTable(int id, UserTable userTable)
        {
            if (id != userTable.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userTable).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTableExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST api/Users/AuthenticationRedirect
        [HttpPost]
        [Route("api/[controller]/AuthenticationRedirect")]
        public IActionResult RedirectSignUp()
        {
            return Redirect("www.google.com");
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Register")]
        public async Task<ActionResult<UserTable>> PostUserTable(UserTable userTable)
        {

            var user = new UserTable
            {
                FirstName = userTable.FirstName,
                LastName = userTable.LastName,
                Email = userTable.Email,
                Password = userTable.Password
            };

            _context.UserTables.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTable", new { id = userTable.UserId }, userTable);
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginModel loginModel)
        {
           
            var result = await _identityService.LoginAsync(loginModel);
            return Ok(result);
        }

        [Route("refresh")]
        [HttpPost]
        public async Task<IActionResult> Refresh([FromBody] TokenModel request)
        {
            var result = await _identityService.RefreshTokenAsync(request);
            return Ok(result);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTable(int id)
        {
            var userTable = await _context.UserTables.FindAsync(id);
            if (userTable == null)
            {
                return NotFound();
            }

            _context.UserTables.Remove(userTable);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTableExists(int id)
        {
            return _context.UserTables.Any(e => e.UserId == id);
        }
    }
}
