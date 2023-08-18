using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pagination.Models;
using Pagination.Services;

namespace Pagination.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        [HttpPost]
        public List<User> CreatedUser(int value)
        {
            return _userServices.CreateAllUsers(value);
        }
        [HttpGet]
        public PageResult<User> GetUserByNam([FromQuery] UserQuery userQuery)
        {
            return _userServices.GetUser(userQuery);
        }

    }
}
