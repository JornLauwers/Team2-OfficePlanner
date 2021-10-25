using OfficePlanner.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Server.Models
{
    public interface IReservationsRepository
    {
        void Create(Reservations reservations);
        void Delete(Reservations reservations);
        Reservations GetById(int id);
        IQueryable<Reservations> GetByDate(DateTime startDate, DateTime endDate);
        void Update(Reservations reservations);
    }
}
