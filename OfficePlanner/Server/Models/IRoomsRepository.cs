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
        RoomVersions<ApplicationUser> GetRoomVersion(int roomId, DateTime ValidOnDate);
        List<RoomVersions<ApplicationUser>> GetAllRoomVersions(int roomId);
        int GetFreeSeats(int roomId, DateTime dateTime);
        void UpdateRoom(RoomsCreateViewModel room, int id);
        List<RoomsReadViewModel> GetAllActiveRooms(DateTime date);
        List<RoomVersionsReadViewModel> GetAllVersions(int id);
        RoomVersionsReadViewModel GetVersionById(int id);
        bool DeleteRoomVersion(int id);
        bool UpdateRoomVersion(RoomVersionsCreateViewModel roomVersion, int id);

    }
}
