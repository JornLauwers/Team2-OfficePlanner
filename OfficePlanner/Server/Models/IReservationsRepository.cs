using OfficePlanner.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Server.Models
{
    public interface IReservationsRepository
    {
        void Create(Reservations<ApplicationUser> reservations);
        void Delete(Reservations<ApplicationUser> reservations);
        Reservations<ApplicationUser> GetById(int id);
        List<ReservationsDTO> GetByDate(DateTime startDate, DateTime endDate);
        void Update(Reservations<ApplicationUser> reservations);
    }
}
