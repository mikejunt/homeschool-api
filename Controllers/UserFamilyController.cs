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
    [Route("api/relations")]
    [ApiController]
    public class UserFamilyController : ControllerBase
    {
        private readonly HSAppDbContext _context;

        public UserFamilyController(HSAppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserFamily
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserToFamily>>> GetUserToFamily()
        {
            return await _context.UserToFamily.ToListAsync();
        }

        // GET: api/relations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<UserToFamily>>> GetUserRelations(int id)
        {
            var query = await _context.UserToFamily
            .Where(obj => obj.UserId == id)
            .ToListAsync();

            return query;
        }

        // PUT: api/relations/edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutUserToFamily(int id, UserToFamily userToFamily)
        {
            if (id != userToFamily.Id)
            {
                return BadRequest();
            }

            _context.Entry(userToFamily).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserToFamilyExists(id))
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

        // POST: api/relations/new
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("new")]
        public async Task<ActionResult<UserToFamily>> PostUserToFamily(UserToFamily userToFamily)
        {
            _context.UserToFamily.Add(userToFamily);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserToFamily", new { id = userToFamily.Id }, userToFamily);
        }

        // DELETE: api/relations/edit/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<UserToFamily>> DeleteUserToFamily(int id)
        {
            var userToFamily = await _context.UserToFamily.FindAsync(id);
            if (userToFamily == null)
            {
                return NotFound();
            }

            _context.UserToFamily.Remove(userToFamily);
            await _context.SaveChangesAsync();

            return userToFamily;
        }

        private bool UserToFamilyExists(int id)
        {
            return _context.UserToFamily.Any(e => e.Id == id);
        }
    }
}
