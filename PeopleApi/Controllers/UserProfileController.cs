using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleApi.Data;
using PeopleApi.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PeopleApi.Controllers
{
    [Route("api/userprofiles")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public UserProfileController(PeopleDbContext context)
        {
            _context = context;
        }

        // Get all profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserProfiles()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        //Get a user profile by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserProfile(string id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile == null) return NotFound();
            return profile;
        }

        // Update user profile
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(string id, UserProfile userProfile)
        {
            if (id != userProfile.Id) return BadRequest();
            _context.Entry(userProfile).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Create a new user profile => by log in automatically
        [HttpPost]
        public async Task<ActionResult<UserProfile>> CreateUserProfile(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUserProfile), new { id = userProfile.Id }, userProfile);
        }

        // Delete a user profile
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var profile = await _context.UserProfiles.FindAsync(id);
            if (profile == null) return NotFound();

            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

