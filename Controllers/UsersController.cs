using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using homeschool_api.Models;

namespace homeschool_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly HSAppDbContext _context;

        public UsersController(HSAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/users/email/mikejunt@gmail.com
        [HttpGet("email/{email}")]
        public async Task<ActionResult<IEnumerable<Users>>> GetUserByEmail(string email)
        {
            var query = await _context.Users
            .Where(
            obj => obj.Email == email)
            .ToListAsync();

            return query;
        }

        // GET: api/users/family?fids=y&fids=z
        [HttpGet("family")]
        public async Task<ActionResult<IEnumerable<FamilyUserData>>> GetFamilyMembers([FromQuery] int[] fids)
        {
            var query = await _context.UserToFamily
            .Join(_context.Users,
            relation => relation.UserId,
                        user => user.Id,
            (relation, user) => (new FamilyUserData
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Photo = user.Photo,
                RelationId = relation.Id,
                FamilyId = relation.FamilyId,
                Role = relation.Role,
                Confirmed = relation.Confirmed
            }))
            .Where(
            obj => fids.Contains(obj.FamilyId))
            .OrderBy(obj => obj.Role)
            .OrderBy(obj => obj.FamilyId)
            .ToListAsync();

            return query;
        }

        // GET: api/users/minors/mikejunt@gmail.com
        [HttpGet("minors/{email}")]
        public async Task<ActionResult<IEnumerable<Users>>> GetMinorsByEmail(string email)
        {
            var query = await _context.Users
            .Where(
            obj => obj.ParentEmail == email && obj.Minor == true)
            .ToListAsync();

            return query;
        }

        // PUT: api/users/update/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, Users users)
        {
            if (id != users.Id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/users/new
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("new")]
        public async Task<ActionResult<Users>> CreateUser(Users users)
        {
            _context.Users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = users.Id }, users);
        }

        // DELETE: api/users/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Users>> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NoContent();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return users;
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
