using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficePlanner.Server.Data;
using OfficePlanner.Shared;

namespace OfficePlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Settings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Setting>>> GetSetting()
        {
            return await _context.Setting.ToListAsync();
        }

        // PUT: api/Settings
        [HttpPut]
        public async Task<IActionResult> PutSetting(Setting setting, int id = 1)
        {
            _context.Entry(setting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettingExists(id))
                {
                    _context.Setting.Add(setting);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool SettingExists(int id)
        {
            return _context.Setting.Any(e => e.Id == id);
        }
    }
}
