using OfficePlanner.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OfficePlanner.Client.Services
{
    public interface IRoomService
    {
        Task<List<RoomsReadViewModel>> GetAllRooms(DateTime date);
        Task<RoomsReadViewModel> GetActiveVersion(int id);
        Task<int> CreateRoom(RoomsCreateViewModel room);
        Task<bool> CreateRoomVersion(RoomVersionsCreateViewModel roomVersion, int id);
        Task<bool> UpdateRoom(RoomsCreateViewModel room, int id);
        Task<List<RoomVersionsReadViewModel>> GetAllVersions(int id);
        Task<RoomVersionsReadViewModel> GetVersionById(int id);
        Task<bool> UpdateRoomVersion(RoomVersionsCreateViewModel roomVersion, int id);
        Task<bool> DeleteRoomVersion(int id);
    }
}
