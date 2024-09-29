using eco_tourism_accommodation.Models;
using Microsoft.EntityFrameworkCore;
using eco_tourism_accommodation.DB;

namespace eco_tourism_accommodation.Services
{
    public interface IRoomInfoService
    {
        Task<IEnumerable<RoomInfo>> GetAllRoomsAsync();
        Task<RoomInfo?> GetRoomByIdAsync(int id);
        Task<RoomInfo> CreateRoomAsync(RoomInfo room);
        Task<RoomInfo?> UpdateRoomAsync(int id, RoomInfo room);
        Task<bool> DeleteRoomAsync(int id);
    }
    public class RoomInfoService : IRoomInfoService
    {
        private readonly EcoTourismAccommodationContext _context;

        public RoomInfoService(EcoTourismAccommodationContext context)
        {
            _context = context;
        }

        // getAll
        public async Task<IEnumerable<RoomInfo>> GetAllRoomsAsync()
        {
            return await _context.RoomInfos.Where(r => !r.IsDeleted).ToListAsync();
        }

        // id get
        public async Task<RoomInfo?> GetRoomByIdAsync(int id)
        {
            return await _context.RoomInfos.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        // creat
        public async Task<RoomInfo> CreateRoomAsync(RoomInfo room)
        {
            room.CreatedAt = DateTime.Now;
            room.UpdatedAt = DateTime.Now;

            _context.RoomInfos.Add(room);
            await _context.SaveChangesAsync();
            return room;
        }

        // update
        public async Task<RoomInfo?> UpdateRoomAsync(int id, RoomInfo room)
        {
            var existingRoom = await _context.RoomInfos.FindAsync(id);

            if (existingRoom == null || existingRoom.IsDeleted)
                return null;

            // update param
            existingRoom.RoomName = room.RoomName;
            existingRoom.RoomType = room.RoomType;
            existingRoom.Description = room.Description;
            existingRoom.Price = room.Price;
            existingRoom.CreditPrice = room.CreditPrice;
            existingRoom.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return existingRoom;
        }

        // delete
        public async Task<bool> DeleteRoomAsync(int id)
        {
            var room = await _context.RoomInfos.FindAsync(id);

            if (room == null || room.IsDeleted)
                return false;

            room.IsDeleted = true;
            room.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
