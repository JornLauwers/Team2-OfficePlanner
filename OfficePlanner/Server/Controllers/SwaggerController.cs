using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OfficePlanner.Server.Data;
using OfficePlanner.Shared;

namespace OfficePlanner.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SwaggerController : Controller
    {
        private readonly ApplicationDbContext db;
        public SwaggerController(ApplicationDbContext context)
        {
            this.db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        //to test the database
        [HttpPost("Create")]
        public async Task<Roles> CreateRole()
        {
            Roles role = new Roles { Name = "User" };
            var newRole = await db.Roles.AddAsync(role);
            await db.SaveChangesAsync();
            return newRole.Entity;
        }
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Roles>> GetRoles()
        {
            return db.Roles;
        }
    }
}
