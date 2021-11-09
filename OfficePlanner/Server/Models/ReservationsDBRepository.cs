using OfficePlanner.Server.Data;
using OfficePlanner.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Server.Models
{
    public class ReservationsDBRepository : IReservationsRepository
    {
        private ApplicationDbContext _context { get; set; }
        public ReservationsDBRepository(ApplicationDbContext applicationDbContext)  
        {
            this._context = applicationDbContext;
        }
        public void Create(Reservations<ApplicationUser> reservations)
        {
            reservations.Rooms = _context.Rooms.Find(reservations.Room);
            reservations.Users =(ApplicationUser) _context.Users.Find(reservations.User.ToString());
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
        public List<Reservations<ApplicationUser>> GetByDate(DateTime startDate, DateTime endDate)
        {
            endDate = endDate.AddDays(1);
            return this._context.Reservations.Where(reservation => reservation.StartDate >= startDate && reservation.EndDate <= endDate).ToList<Reservations<ApplicationUser>>();
        }
        public void Update(Reservations<ApplicationUser> reservations)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<Shared.Reservations<ApplicationUser>> entityEntry = this._context.Reservations.Update(reservations);
            this._context.SaveChanges();
        }
    }
}
