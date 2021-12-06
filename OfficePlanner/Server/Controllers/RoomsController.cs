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
        public IActionResult GetAllRooms([FromQuery] DateTime date)
        {
            var roomsList = roomsRepository.GetAllActiveRooms(date);

            return Ok(roomsList.ToArray());
        }

        // GET api/Rooms/5
        [HttpGet("Active/{id}")]
        public IActionResult GetActiveVersion(int id)
        {
            var roomVersion = roomsRepository.GetRoomVersion(id, DateTime.Now);
            var room = roomsRepository.GetById(id);
            if (roomVersion != null && room != null)
            {
                var roomModel = new RoomsReadViewModel
                {
                    AvailableSeats = roomVersion.AvailableSeats,
                    EndDate = roomVersion.EndDate,
                    StartDate = roomVersion.StartDate,
                    Name = room.Name,
                    Type = room.Type,
                    FreeSeats = roomsRepository.GetFreeSeats(id, DateTime.Now),
                    Id = room.Id
                };
                return Ok(roomModel);
            }
            return NotFound();
        }

        // GET api/Rooms/Versions/5
        [HttpGet("Versions/{id}")]
        public IActionResult GetAllVersions(int id)
        {
            var roomVersions = roomsRepository.GetAllVersions(id);
            if (roomVersions != null)
            {
                return Ok(roomVersions);
            }
            return NotFound();
        }


        // GET api/Rooms/VersionDetails/5
        [HttpGet("VersionDetails/{id}")]
        public IActionResult GetVersionDetails(int id)
        {
            var roomVersion = roomsRepository.GetVersionById(id);
            if (roomVersion != null)
            {
                return Ok(roomVersion);
            }
            return NotFound();
        }

        // POST api/Rooms
        [HttpPost]
        public IActionResult CreateRoom(RoomsCreateViewModel room)
        {
            if (ModelState.IsValid)
            {
                int createdId = this.roomsRepository.CreateRoom(room);
                // Return the Id in the db of the created room, might be useful for creating version
                return CreatedAtAction("CreateRoom", createdId);
            }
            return BadRequest(ModelState);
        }

        // POST api/Rooms/AddVersion/5
        [HttpPost("AddVersion/{roomId}")]
        public IActionResult CreateRoomVersion(RoomVersionsCreateViewModel version, [FromRoute] int roomId)
        {
            if (ModelState.IsValid)
            {
                this.roomsRepository.CreateRoomVersion(version, roomId);
                return Ok(ModelState);
            }
            return BadRequest(ModelState);
        }

        // PUT api/Rooms/5
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody]RoomsCreateViewModel room)
        {
            if (ModelState.IsValid)
            {
                this.roomsRepository.UpdateRoom(room, id);
                return Ok(ModelState);
            }
            return BadRequest(ModelState);
        }

        // PUT api/Rooms/Versions/5
        [HttpPut("Versions/{id}")]
        public IActionResult UpdateRoomVersion(int id, [FromBody] RoomVersionsCreateViewModel room)
        {
            if (ModelState.IsValid)
            {
                this.roomsRepository.UpdateRoomVersion(room, id);
                return Ok(ModelState);
            }
            return BadRequest(ModelState);
        }

        // DELETE api/Rooms/Versions/5
        [HttpDelete("Versions/{id}")]
        public IActionResult DeleteRoomVersion(int id)
        {
            if (ModelState.IsValid)
            {
                this.roomsRepository.DeleteRoomVersion(id);
                return Ok(ModelState);
            }
            return BadRequest(ModelState);
        }
    }
}
