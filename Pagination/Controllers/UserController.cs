//using Bogus;
using FluentValidation;
using FluentValidation.Results;
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
        private readonly IValidator<UserQuery> _validator;

        public UserController(IUserServices userServices, IValidator<UserQuery> validator)
        {
            _userServices = userServices;
            _validator = validator;
        }
        [HttpPost]
        public List<User> CreatedUser(int value)
        {
            return _userServices.CreateAllUsers(value);
        }
        [HttpGet]
        public ActionResult<PageResult<User>> GetUserByNam([FromQuery] UserQuery userQuery)
        {
            ValidationResult result = _validator.Validate(userQuery);

            if (result.IsValid)
            {
                return _userServices.GetUser(userQuery);
            }
            return BadRequest(result);
        }

    }
}
