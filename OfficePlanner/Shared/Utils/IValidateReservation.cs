using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficePlanner.Shared.ViewModels;

namespace OfficePlanner.Shared.Utils
{
    public interface IValidateReservation
    {
        Task<bool> Validate(ReservationCreateViewModel reservation);
    }
}
