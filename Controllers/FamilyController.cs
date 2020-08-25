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
    public class FamilyController : ControllerBase
    {
        private readonly HSAppDbContext _context;

        public FamilyController(HSAppDbContext context)
        {
            _context = context;
        }

        // GET: api/Family
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Family>>> GetFamily()
        {
            return await _context.Family.ToListAsync();
        }

        // GET: api/family/user/5
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<FamilyMembership>>> GetFamilyForUser(int id)
        {
            var query = await _context.Family
                        .Join(_context.UserToFamily,
                            families => families.Id,
                            relation => relation.FamilyId,
                        (families, relation) => (new FamilyMembership
                        {
                            FamilyId = families.Id,
                            AdminId = families.AdminId,
                            Name = families.Name,
                            RelationId = relation.Id,
                            UserId = relation.UserId,
                            Role = relation.Role,
                            Confirmed = relation.Confirmed
                        }))
                        .Where(
                        obj => obj.UserId == id)
                        .OrderBy(obj => obj.Role)
                        .ToListAsync();

            if (query.Count() == 0)
            {
                return NoContent();
            }
            return query;
        }

        // GET: api/family/minors?uids=1&uids=2
                [HttpGet("minors")]
        public async Task<ActionResult<IEnumerable<FamilyMembership>>> GetFamilyForMinors([FromQuery] int[] uids)
        {
            var query = await _context.Family
                        .Join(_context.UserToFamily,
                            families => families.Id,
                            relation => relation.FamilyId,
                        (families, relation) => (new FamilyMembership
                        {
                            FamilyId = families.Id,
                            AdminId = families.AdminId,
                            Name = families.Name,
                            RelationId = relation.Id,
                            UserId = relation.UserId,
                            Role = relation.Role,
                            Confirmed = relation.Confirmed
                        }))
                        .Where(
                        obj => uids.Contains(obj.UserId))
                        .OrderBy(obj => obj.UserId)
                        .ToListAsync();

            if (query.Count() == 0)
            {
                return NoContent();
            }
            return query;
        }

        // PUT: api/family/edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> PutFamily(int id, Family family)
        {
            if (id != family.Id)
            {
                return BadRequest();
            }

            _context.Entry(family).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FamilyExists(id))
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

        // POST: api/family/new
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("new")]
        public async Task<ActionResult<Family>> PostFamily(Family family)
        {
            _context.Family.Add(family);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFamily", new { id = family.Id }, family);
        }

        // DELETE: api/family/delete/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<Family>> DeleteFamily(int id)
        {
            var family = await _context.Family.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            _context.Family.Remove(family);
            await _context.SaveChangesAsync();

            return family;
        }

        private bool FamilyExists(int id)
        {
            return _context.Family.Any(e => e.Id == id);
        }
    }
}
