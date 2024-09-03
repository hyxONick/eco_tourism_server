using System.Collections.Generic;
using System.Threading.Tasks;
using eco_tourism_tourist.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using eco_tourism_tourist.DB;

namespace eco_tourism_tourist.Services
{
    public interface ITouristOrderInfoService
    {
        Task CreateAsync(TouristOrderInfo newOrder);
        Task<List<TouristOrderInfo>> GetAllAsync();
        Task<TouristOrderInfo?> GetByIdAsync(int id);
        Task UpdateAsync(int id, TouristOrderInfo updatedOrder);
        Task DeleteAsync(int id);
        Task<List<TouristOrderInfo>> SearchAsync(string searchTerm);
    }

    public class TouristOrderInfoService : ITouristOrderInfoService
    {
        private readonly EcoTourismTouristContext _context;

        public TouristOrderInfoService(EcoTourismTouristContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(TouristOrderInfo newOrder)
        {
            _context.TouristOrderInfos.Add(newOrder);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TouristOrderInfo>> GetAllAsync()
        {
            return await _context.TouristOrderInfos
                .Where(o => !o.IsDeleted)
                .ToListAsync();
        }

        public async Task<TouristOrderInfo?> GetByIdAsync(int id)
        {
            return await _context.TouristOrderInfos
                .Where(o => o.Id == id && !o.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(int id, TouristOrderInfo updatedOrder)
        {
            var existingOrder = await _context.TouristOrderInfos.FindAsync(id);
            if (existingOrder != null)
            {
                existingOrder.SceneId = updatedOrder.SceneId;
                existingOrder.GuestId = updatedOrder.GuestId;
                existingOrder.Memo = updatedOrder.Memo;
                existingOrder.Amount = updatedOrder.Amount;
                existingOrder.UpdatedAt = updatedOrder.UpdatedAt;
                existingOrder.IsDeleted = updatedOrder.IsDeleted;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var existingOrder = await _context.TouristOrderInfos
                .Where(o => o.Id == id && !o.IsDeleted)
                .FirstOrDefaultAsync();
            if (existingOrder != null)
            {
                existingOrder.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<TouristOrderInfo>> SearchAsync(string searchTerm)
        {
            return await _context.TouristOrderInfos
                .Where(o => !o.IsDeleted &&
                            ((o.Memo ?? string.Empty).Contains(searchTerm) || o.Amount.ToString().Contains(searchTerm)))
                .ToListAsync();
        }
    }
}
