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
        public IActionResult GetAllRooms([FromQuery] DateTime dateTime)
        {
            var roomsList = roomsRepository.GetRooms();
            var roomModels = new List<RoomsReadViewModel>();

            if (dateTime == DateTime.MinValue)
            {
                dateTime = DateTime.Now.Date;
            }

            foreach (var room in roomsList)
            {
                if (roomsRepository.GetActiveRoomVersion(room.Id, dateTime) != null)
                {
                    roomModels.Add(new RoomsReadViewModel
                    {
                        Name = room.Name,
                        Type = room.Type,
                        AvailableSeats = roomsRepository.GetActiveRoomVersion(room.Id, dateTime).AvailableSeats,
                        FreeSeats = roomsRepository.GetFreeSeats(room.Id, dateTime),
                        EndDate = roomsRepository.GetActiveRoomVersion(room.Id, dateTime).EndDate,
                        StartDate = roomsRepository.GetActiveRoomVersion(room.Id, dateTime).StartDate,
                        Id = roomsRepository.GetActiveRoomVersion(room.Id, dateTime).Id
                    });
                }
            }

            return Ok(roomModels.ToArray());
        }

        // GET api/Rooms/5
        [HttpGet("Active/{id}")]
        public IActionResult GetActiveVersion(int id)
        {
            var roomVersion = roomsRepository.GetActiveRoomVersion(id, DateTime.Now);
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
        public IActionResult UpdateRoom(int id, [FromBody]RoomsCreateViewModel room )
        {
            if (ModelState.IsValid)
            {
                var rooms = new Rooms<ApplicationUser>
                {
                    Id = id,
                    Name = room.Name,
                    Type = room.Type
                };

                this.roomsRepository.UpdateRoom(rooms);
                return Ok(ModelState);
            }
            return BadRequest(ModelState);
        }
    }
}
