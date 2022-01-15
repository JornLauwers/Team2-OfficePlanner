using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficePlanner.Server.Models;
using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<IdentityUser> userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        // [Authorize]
        [HttpPut("GiveAdmin")]
        public async Task<IActionResult> AddRoleToUser([FromQuery] string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);
                userManager.AddToRoleAsync(user, "Administrator").Wait();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetUsers")]
        public async Task<List<IdentityUser>> GetUsers()
        {
            var users = await userManager.Users.ToListAsync();
            return users;
        }

    }
}
