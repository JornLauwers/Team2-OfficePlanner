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
        private ApplicationDbContext applicationDbContext { get; set; }
        public ReservationsDBRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }
        public void Create(Reservations reservations)
        {
            this.applicationDbContext.Reservations.Add(reservations);
            this.applicationDbContext.SaveChanges();
        }
        public void Delete(Reservations reservations)
        {
            this.applicationDbContext.Reservations.Remove(reservations);
            this.applicationDbContext.SaveChanges();
        }
        public Reservations GetById(int id)
        {
            return this.applicationDbContext.Reservations.Find(id);
        }
        public IQueryable<Reservations> GetByDate(DateTime startDate, DateTime endDate)
        {
            //hoop dit werkt
            return this.applicationDbContext.Reservations.Where(reservation => reservation.StartDate >= startDate && reservation.EndDate <= endDate);
        }
        public void Update(Reservations reservations)
        {
            this.applicationDbContext.Reservations.Update(reservations);
            this.applicationDbContext.SaveChanges();
        }
    }
}
