using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrabalhoBackEnd.BLL;
using TrabalhoBackEnd.Errors;
using TrabalhoBackEnd.Models;
using TrabalhoBackEnd.Models.Requests;
using TrabalhoBackEnd.Models.Responses;

namespace TrabalhoBackEnd.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserService _service;

        public UsersController(UserService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("cadastro")]
        public ActionResult<UserResponse> CreateUser([FromBody] CreateUserRequests createUserRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = createUserRequest.ToUser();
                var createdUser = _service.Insert(user);
                var userResponse = new UserResponse(createdUser);
                return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, userResponse);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = _service.Login(loginRequest.Email, loginRequest.Password);
            if (response == null)
                return Unauthorized();

            return Ok(response);
        }

        [HttpGet]
        public ActionResult<List<UserResponse>> ListUsers([FromQuery] string description)
        {
            try
            {
                var users = _service.List(description);
                var userResponses = new List<UserResponse>();
    
                foreach (var user in users)
                {
                    userResponses.Add(new UserResponse(user));
                }

                return Ok(userResponses);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponse> GetUserById(long id)
        {
            var user = _service.FindByIdOrNull(id);
            if (user == null)
                return NotFound();

            var userResponse = new UserResponse(user);
            return Ok(userResponse);
        }

        
        
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(long id)
        {
            try
            {
                var deletedUser = _service.Delete(id);
                if (deletedUser == null)
                    return NotFound();

                return Ok();
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

       
    }
}
