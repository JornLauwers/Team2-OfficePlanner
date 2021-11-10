using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OfficePlanner.Server.Models;
using OfficePlanner.Shared;
using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OfficePlanner.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsRepository roomsRepository;
        private IMapper _mapper { get; set; }
        public RoomsController(IRoomsRepository roomsRepository, IMapper mapper)
        {
            this.roomsRepository = roomsRepository;
            this._mapper = mapper;
        }

        // GET: api/Rooms
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RoomsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/Rooms
        [HttpPost]
        public IActionResult CreateRoom(RoomsCreateViewModel room)
        {
            if (ModelState.IsValid)
            {
                this.roomsRepository.Create(room);
                return Ok(ModelState);
            }
            return BadRequest(ModelState);
        }

        // PUT api/<RoomsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RoomsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
