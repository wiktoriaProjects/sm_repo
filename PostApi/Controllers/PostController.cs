using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostApi.Data;
using PostApi.DTO;
using PostApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PostApi.Controllers
{
    [Route("api/posts")]
    [ApiController]
    ///[Authorize]
    public class PostController : ControllerBase
    {
        private readonly PostDbContext _context;
        private readonly IMapper _mapper;

        public PostController(PostDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();
            return _mapper.Map<List<PostDto>>(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPostById(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == id);
            if (post == null) return NotFound();

            return _mapper.Map<PostDto>(post);
        }
        // get posts by specific user 
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetUserPosts(string userId)
        {
            var posts = await _context.Posts
                .Where(post => post.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<PostDto>>(posts);
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> CreatePost(CreatePostDto postDto)
        {

            if (string.IsNullOrEmpty(postDto.UserId))
            {
                return BadRequest("UserId is required.");
            }

            var post = _mapper.Map<Post>(postDto);
            post.CreatedAt = DateTime.UtcNow;

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserPosts), new { userId = post.UserId }, _mapper.Map<PostDto>(post));
            
            //----------testing no user id
            //var post = _mapper.Map<Post>(postDto);
            //post.UserId = "test-user"; // Hardcoded test user for now
            //post.CreatedAt = System.DateTime.UtcNow;

            //_context.Posts.Add(post);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction(nameof(GetPostById), new { id = post.PostId }, _mapper.Map<PostDto>(post));
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.PostId == id);
            if (post == null) return NotFound();

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}





