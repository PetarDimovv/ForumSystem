using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackOverflow.Exceptions;
using StackOverflow.Helpers;
using StackOverflow.Models;
using StackOverflow.Models.DTOs;
using StackOverflow.Services.Interface;

namespace StackOverflow.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserAPIController : ControllerBase
    {
        private readonly AuthManager authManager;
        private readonly ModelMapper modelMapper;
        private readonly IUserService userService;
        public UserAPIController(IUserService userService, ModelMapper modelMapper, AuthManager authManager)
        {
            this.userService = userService;
            this.modelMapper = modelMapper;
            this.authManager = authManager;
        }
        [HttpPut("")]
        public IActionResult UpdateUser([FromHeader] string username, [FromBody] RegisterViewModel dto)
        {
            try
            {
                User user = this.modelMapper.MapUser(dto);

                User updatedUser = this.userService.UpdateUser(user.Id, user); //TODO

                return this.StatusCode(StatusCodes.Status200OK, updatedUser);
            }
            catch (UnauthorizedOperationException e)
            {
                return this.StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (EntityNotFoundException e)
            {
                return this.StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (DuplicateEntityException e)
            {
                return this.StatusCode(StatusCodes.Status409Conflict, e.Message);
            }

        }
    }
}
