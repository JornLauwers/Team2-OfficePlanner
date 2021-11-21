using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficePlanner.Server.Data;
using OfficePlanner.Server.Models;
using OfficePlanner.Shared;
using OfficePlanner.Shared.ViewModels;

namespace OfficePlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsRepository settingsRepository;


        public SettingsController(ISettingsRepository settingsRepository)
        {
            this.settingsRepository = settingsRepository;
        }

        // GET: api/Settings/all
        [HttpGet("all")]
        public ActionResult<List<SettingReadViewModel>> GetAllProperties()
        {
            return Ok(settingsRepository.GetAllSettings());
        }
        // GET: api/Settings/active?date=2021-11-11
        [HttpGet("active")]
        public ActionResult<SettingReadViewModel> GetActiveProperties([FromQuery] DateTime date)
        {
            var settingReadViewModel = settingsRepository.GetActiveProperties(date);
            if (settingReadViewModel != null)
            {
                return Ok(settingReadViewModel);
            }
            else
            {
                return NotFound();
            }
            
        }

        // GET: api/Settings/5
        [HttpGet("{id}")]
        public ActionResult<SettingReadViewModel> GetSettingById([FromRoute] int id)
        {
            var setting = settingsRepository.GetById(id);

            if (setting != null)
            {
                return Ok(setting);
            }
            else
            {
                return NotFound();
            }
            
        }

        // PUT: api/Settings
        [HttpPut("{id}")]
        public ActionResult<Setting> PutSetting(SettingUpdateViewModel setting, [FromRoute] int id)
        {
            bool success = settingsRepository.UpdateSetting(setting, id);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        // POST: api/Settings
        [HttpPost]
        public ActionResult<SettingCreateViewModel> PostSetting(SettingCreateViewModel settings)
        {
            bool success = settingsRepository.CreateSetting(settings);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }  
        }

        // DELETE: api/Settings/5
        [HttpDelete("{id}")]
        public ActionResult<Setting> DeleteSetting([FromRoute] int id)
        {
            bool success = settingsRepository.DeleteSetting(id);
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
