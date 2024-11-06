using Microsoft.AspNetCore.Mvc;
using MockServers.Model;
using MockServers.Repositories;
using System.Net.Http;

namespace MockServers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        //public UserController()
        //{
        //    _userRepository = new UserRepository(); // Normally injected, but simplified for this example
        //}
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        // GET: api/Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
           // var geoLocationUrl = $"https://localhost:7243/api/GeoLocation?city={city}";
            //var geoResponse = await _httpClient.GetAsync(geoLocationUrl);

            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null || user.UserId != id)
            {
                return BadRequest();
            }

            var success = _userRepository.UpdateUser(user);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var success = _userRepository.DeleteUser(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
