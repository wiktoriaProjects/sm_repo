using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleApi.Data;
using PeopleApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleApi.Controllers
{
    [Route("api/friendships")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly PeopleDbContext _context;

        public FriendController(PeopleDbContext context)
        {
            _context = context;
        }

        // send the request
        [HttpPost("send")]
        public async Task<IActionResult> SendFriendRequest([FromBody] Friend request)
        {
            var existingRequest = await _context.Friend
                .FirstOrDefaultAsync(f => f.UserId == request.UserId && f.FriendId == request.FriendId);

            if (existingRequest != null)
                return BadRequest("Friend request already sent.");

            _context.Friend.Add(request);
            await _context.SaveChangesAsync();
            return Ok("Friend request sent.");
        }

        // Accept a Friend Request
        [HttpPut("accept/{requestId}")]
        public async Task<IActionResult> AcceptFriendRequest(int requestId)
        {
            var friendship = await _context.Friend.FindAsync(requestId);
            if (friendship == null)
                return NotFound("Friend request not found.");

            friendship.Status = FriendStatus.Accepted;
            await _context.SaveChangesAsync();
            return Ok("Friend request accepted.");
        }

        // reject a Friend Request
        

        // list of friends
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUserFriends(string userId)
        {
            var friends = await _context.Friend
                .Where(f => f.UserId == userId && f.Status == FriendStatus.Accepted)
                .Select(f => f.FriendProfile)
                .ToListAsync();

            return friends;
        }

       
    }
}

