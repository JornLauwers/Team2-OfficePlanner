using OfficePlanner.Shared;
using OfficePlanner.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OfficePlanner.Shared.ViewModels;

namespace OfficePlanner.Server.Models
{
    public class RoomsDBRepository : IRoomsRepository
    {
        private ApplicationDbContext _context { get; set; }

        public RoomsDBRepository(ApplicationDbContext applicationDbContext)    
        {
            this._context = applicationDbContext;
        }

        public void Create(RoomsCreateViewModel rooms)
        {



            var roomVersion = new RoomVersions<ApplicationUser>
            {
                AvailableSeats = rooms.AvailableSeats,
                RoomId = _context.Rooms.OrderByDescending(i => i.Id).FirstOrDefault().Id + 1,
                StartDate = DateTime.Now,
                EndDate = DateTime.MaxValue
            };

            var newRoom = new Rooms<ApplicationUser>
            {
                Name = rooms.Name,
                Type = rooms.Type,
                RoomVersions = (ICollection<RoomVersions<ApplicationUser>>)roomVersion,

            };

            _context.Add(rooms);
            //_context.Add(roomVersion);
            _context.SaveChanges();
        }

        public void Delete(Rooms<ApplicationUser> rooms)
        {
            throw new NotImplementedException();
        }

        public Rooms<ApplicationUser> GetById(int id)
        {
            throw new NotImplementedException();
        }



        public void Update(Rooms<ApplicationUser> rooms)
        {
            throw new NotImplementedException();
        }

        public ICollection<RoomVersions<ApplicationUser>> GetFreeRooms()
        {
            throw new NotImplementedException();
        }
    }
}
