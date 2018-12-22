using System;
using IB.Services.Interface.Interfaces;
using InternetBanking.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetBanking.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("api/users")]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("api/profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var userId = User.GetUserId();
            var profile = _userService.GetProfile(userId);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }

        [HttpGet("api/profile/{userId:guid}")]
        //[RequirePermission(Permission.GetUsers)]
        public IActionResult GetProfile(Guid userId)
        {
            var profile = _userService.GetProfile(userId);

            if (profile == null)
            {
                return NotFound();
            }

            return Ok(profile);
        }
    }
}
