using OfficePlanner.Shared;
using OfficePlanner.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using OfficePlanner.Shared.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace OfficePlanner.Server.Models
{
    public class RoomsDBRepository : IRoomsRepository
    {
        private ApplicationDbContext _context { get; set; }

        public RoomsDBRepository(ApplicationDbContext applicationDbContext)    
        {
            this._context = applicationDbContext;
        }

        public int CreateRoom(RoomsCreateViewModel rooms)
        {

            var newRoom = new Rooms<ApplicationUser>
            {
                Name = rooms.Name,
                Type = rooms.Type

            };

            _context.Add(newRoom);
            _context.SaveChanges();
            return newRoom.Id;
        }

        public bool CreateRoomVersion(RoomVersionsCreateViewModel roomVersion, int room)
        {
            var rooms = _context.Rooms.Find(room);

            if (rooms == null)
            {
                return false;
            }

            if (roomVersion.StartDate == DateTime.MinValue)
            {
                roomVersion.StartDate = DateTime.Now.Date;
            }
            else
            {
                var oldVersion = GetActiveRoomVersion(rooms.Id, roomVersion.StartDate);

                if (oldVersion != null)
                {
                    oldVersion.EndDate = roomVersion.StartDate.AddDays(-1);
                    _context.Entry(oldVersion).State = EntityState.Modified;
                }

                var newVersion = new RoomVersions<ApplicationUser>
                {
                    AvailableSeats = roomVersion.AvailableSeats,
                    EndDate = DateTime.MaxValue,
                    RoomId = rooms.Id,
                    StartDate = roomVersion.StartDate
                };
                _context.Add(newVersion);
                _context.SaveChanges();
                
            }
            return true;
        }

        public Rooms<ApplicationUser> GetById(int id)
        {
            var room = _context.Rooms.Find(id);
            return room;
        }



        public void UpdateRoom(Rooms<ApplicationUser> rooms)
        {
            _context.Entry(rooms).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public int GetFreeSeats(int roomId, DateTime dateTime)
        {
            var roomVersion = GetActiveRoomVersion(roomId, dateTime);
            var reservations = _context.Reservations.Where(r => r.Room == roomId && r.EndDate > dateTime && r.StartDate < dateTime);
            var amountOfReservations = reservations.Count();

            var freeSeats = roomVersion.AvailableSeats - amountOfReservations;

            return freeSeats;
        }

        public RoomVersions<ApplicationUser> GetActiveRoomVersion(int roomId, DateTime validOnDate)
        {
            var roomVersion = _context.RoomVersions.FirstOrDefault(r => r.RoomId == roomId && r.EndDate.Date >= validOnDate.Date && r.StartDate.Date <= validOnDate.Date);

            return roomVersion;
        }

        public List<Rooms<ApplicationUser>> GetRooms()
        {
            return _context.Rooms.ToList();
        }
    }
}
