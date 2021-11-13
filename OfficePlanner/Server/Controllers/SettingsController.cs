using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficePlanner.Server.Data;
using OfficePlanner.Shared;
using OfficePlanner.Shared.ViewModels;

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
        // GET: api/Settings
        [HttpGet("active")]
        public ActionResult<Setting> GetActiveSettings()
        {
            var currentSetting = _context.Setting.FirstOrDefault(s => s.FromDate <= DateTime.Now.Date && s.UntilDate.Date >= DateTime.Now.Date);
            dynamic activeSetting = JsonConvert.DeserializeObject(currentSetting.Settings);

            SettingReadViewModel settingReadViewModel = new SettingReadViewModel
            {
                DaysRequiredInOffice = activeSetting.DaysRequiredInOffice,
                Holidays = activeSetting.Holidays.ToObject<DateTime[]>(),
                Workhours = activeSetting.Workhours.ToObject<Workhours>()
            };
            return Ok(settingReadViewModel); 
        }

        // GET: api/Settings
        [HttpGet("{id}")]
        public ActionResult<Setting> GetSettingById([FromRoute] int id)
        {
            return _context.Setting.FirstOrDefault(e => e.Id == id);
        }

        // PUT: api/Settings
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSetting(Setting setting, [FromRoute] int id)
        {
            if (setting.FromDate <= DateTime.Now)
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(setting).State = EntityState.Modified;


                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SettingExists(setting.Id))
                    {
                        _context.Setting.Add(setting);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return Ok();
        }


        // POST: api/Settings
        [HttpPost]
        public async Task<IActionResult> PostSetting(SettingCreateViewModel properties)
        {
            Properties currentProperties = new Properties
            {
                DaysRequiredInOffice = properties.DaysRequiredInOffice,
                Holidays = properties.Holidays,
                Workhours = properties.Workhours
            };

            // Set UntilDate of currently active setting version to FromDate of new one -1d for versioning if previous version exists
            Setting oldActiveSetting = _context.Setting.FirstOrDefault(s => s.UntilDate == DateTime.MaxValue);
            if (oldActiveSetting != null)
            {
                oldActiveSetting.UntilDate = properties.FromDate.AddDays(-1);
                _context.Entry(oldActiveSetting).State = EntityState.Modified;
            }

            Setting setting = new Setting
            {
                FromDate = properties.FromDate,
                UntilDate = DateTime.MaxValue,
                Settings = JsonConvert.SerializeObject(currentProperties)
            };
            await _context.Setting.AddAsync(setting);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        private bool SettingExists(int id)
        {
            return _context.Setting.Any(e => e.Id == id);
        }
    }
}
