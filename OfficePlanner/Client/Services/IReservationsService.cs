using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficePlanner.Shared.ViewModels;
using OfficePlanner.Shared;

namespace OfficePlanner.Client.Services
{
    public interface IReservationsService
    {
        Task<bool> CreateReservation(ReservationCreateViewModel reservationCreateViewModel);
        Task<ReservationsDTO> GetReservationById(int id);
        Task<ReservationListView> GetReservationByDate(DateTime startDate, DateTime endDate);
        Task UpdateReservation(ReservationUpdateViewModel reservationUpdateViewModel);
        Task<bool> DeleteReservation(int id);
        Task<bool> Validate(ReservationCreateViewModel reservation);
    }
}
