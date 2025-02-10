using IdentityApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _userdbcontext;
        public UserController(UserDbContext userDbContext) 
 
        {
            _userdbcontext = userDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _userdbcontext.Users;
        }

        [HttpGet("{UserId:int}")]
        public async Task <ActionResult<User>> GetById(int UserId)
        {
            var user = await _userdbcontext.Users.FindAsync(UserId);
            return user;
        }

        [HttpPost]
        public async Task<ActionResult> Create(User user)
        {
            await _userdbcontext.Users.AddAsync(user);
            await _userdbcontext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(User user)
        {
            _userdbcontext.Users.Update(user);
            await _userdbcontext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{UserId:int}")]

        public async Task<ActionResult> Delete(int UserId)
        {
            var user = await _userdbcontext.Users.FindAsync(UserId);
            _userdbcontext.Users.Remove(user);
            await _userdbcontext.SaveChangesAsync();
            return Ok();

        }


    }
}
