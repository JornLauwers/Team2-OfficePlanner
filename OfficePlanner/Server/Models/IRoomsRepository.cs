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
        int CreateRoom(RoomsCreateViewModel room);
        bool CreateRoomVersion(RoomVersionsCreateViewModel roomVersion, int room);
        Rooms<ApplicationUser> GetById(int id);
        RoomVersions<ApplicationUser> GetActiveRoomVersion(int roomId, DateTime ValidOnDate);
        int GetFreeSeats(int roomId, DateTime dateTime);
        void UpdateRoom(Rooms<ApplicationUser> rooms);
        List<Rooms<ApplicationUser>> GetRooms();

    }
}
