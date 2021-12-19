using OfficePlanner.Server.Data;
using OfficePlanner.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace OfficePlanner.Server.Models
{
    public class ReservationsDBRepository : IReservationsRepository
    {
        private ApplicationDbContext _context { get; set; }
        private IMapper _mapper { get; set; }
        public ReservationsDBRepository(ApplicationDbContext applicationDbContext, IMapper mapper)  
        {
            this._context = applicationDbContext;
            this._mapper = mapper;
        }
        public void Create(Reservations<ApplicationUser> reservations)
        {
            reservations.Rooms = _context.Rooms.Find(reservations.Room);
            reservations.Users = _context.Users.Find(reservations.User.ToString());
            this._context.Reservations.Add(reservations);
            this._context.SaveChanges();
        }
        public void Delete(Reservations<ApplicationUser> reservations)
        {
            this._context.Reservations.Remove(reservations);
            this._context.SaveChanges();
        }
        public Reservations<ApplicationUser> GetById(int id)
        {
            return this._context.Reservations.Find(id);
        }
        public List<ReservationsDTO> GetByDate(DateTime startDate, DateTime endDate)
        {
            endDate = endDate.AddDays(1);
            var listRes = this._context.Reservations.Where(reservation => reservation.StartDate >= startDate && reservation.EndDate <= endDate).ToList<Reservations<ApplicationUser>>();
            var newlist = new List<ReservationsDTO>();
            for (int i = 0; i < listRes.Count; i++)
            {
                newlist.Add(new ReservationsDTO { 
                    Room = listRes[i].Room,
                    User = listRes[i].User,
                    UserName = _context.Users.Find(listRes[i].User.ToString()).UserName,
                    RoomName = _context.Rooms.Find(listRes[i].Room).Name,
                    StartDate = listRes[i].StartDate,
                    EndDate = listRes[i].EndDate,
                    Id = listRes[i].Id
                });
            }
            return newlist;
        }
        public void Update(Reservations<ApplicationUser> reservations)
        {
            reservations.Rooms = _context.Rooms.Find(reservations.Room);
            reservations.Users = _context.Users.Find(reservations.User.ToString());
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Shared.Reservations<ApplicationUser>> entityEntry = this._context.Reservations.Update(reservations);
            this._context.SaveChanges();
        }
    }
}
