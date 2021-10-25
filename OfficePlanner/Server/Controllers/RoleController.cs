using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OfficePlanner.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }


        // TODO: POST method for now, probably better to have the 2 roles (user, administrator) created by the application itself if they don't yet exist
        [HttpPost("CreateRole")]
        public async Task<IActionResult> Create(ApplicationRole role)
        {
            var roleExists = await roleManager.RoleExistsAsync(role.RoleName);
            if (!roleExists)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role.RoleName));
                return Ok();
            }

            return BadRequest();            
        }

    }
}
