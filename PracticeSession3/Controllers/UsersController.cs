using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PracticeSession3.Models;
using PracticeSession3.Services;

namespace PracticeSession3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        //private readonly ILogger<UsersController> _logger;

        public UsersController(IUserService userService) //, ILogger<UsersController> logger)
        {
            _userService = userService;
            //_logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserProfile>>> GetAllUsers() 
        {
            //_logger.LogInformation("Get request made to get all users.");
            var users = await _userService.GetUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUserByID(int id)
        {
            var user = await _userService.GetUser(id);
            if(user == null)
                return NotFound("User Not Found...");
            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<bool>> AddUser([FromBody]UserProfile user)
        {
            await _userService.AddUser(user);
            return Created("User Added successfully",true);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result) 
                return Ok(true);
            else
                return NotFound("User Not Found with given Id");
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<UserProfile>>> UpdateUser(int id, [FromBody]UserProfile user)
        {
            var result = await _userService.UpdateUser(id, user);
            if (result is null) return NotFound("User not found with given id.");
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<List<UserProfile>>> UpdateByID(int id, JsonPatchDocument<UserProfile> user)
        {
            try
            {
                var result = await _userService.UpdateUserById(id, user);
                if (result is null) return NotFound("User not found with given id.");
                return Ok(result);
            }
            catch (Exception ex) { Console.WriteLine(ex); return NotFound(); }
        }
        [HttpPatch("UpdateByName/{name}")]
        public async Task<ActionResult<List<UserProfile>>> UpdateByName(string name, JsonPatchDocument<UserProfile> user)
        {
            var result = await _userService.UpdateUserByName(name, user);
            if (result is null) return NotFound("User not found with given id.");
            return Ok(result);
        }

    }
}
