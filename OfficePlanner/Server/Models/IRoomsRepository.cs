using OfficePlanner.Shared;
using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Server.Models
{
    public interface IRoomsRepository
    {
        void Create(RoomsCreateViewModel room);
        void Delete(Rooms<ApplicationUser> rooms);
        Rooms<ApplicationUser> GetById(int id);
        ICollection<RoomVersions<ApplicationUser>> GetFreeRooms();
        void Update(Rooms<ApplicationUser> rooms);
    }
}
