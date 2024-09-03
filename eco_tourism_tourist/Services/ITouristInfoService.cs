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
    public interface ITouristInfoService
    {
        Task<List<TouristInfo>> GetAllAsync();
        Task<TouristInfo?> GetByIdAsync(int id);
        Task CreateAsync(TouristInfo newTouristInfo);
        Task UpdateAsync(int id, TouristInfo updatedTouristInfo);
        Task DeleteAsync(int id);
    }

    public class TouristInfoService(EcoTourismTouristContext context) : ITouristInfoService
    {
        private readonly EcoTourismTouristContext _context = context;

        public async Task<List<TouristInfo>> GetAllAsync()
        {
            return await _context.TouristInfos
                .Where(t => !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<TouristInfo?> GetByIdAsync(int id)
        {
            return await _context.TouristInfos
                .Where(t => t.Id == id && !t.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TouristInfo newTouristInfo)
        {
            _context.TouristInfos.Add(newTouristInfo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, TouristInfo updatedTouristInfo)
        {
            var touristInfo = await _context.TouristInfos.FindAsync(id);
            if (touristInfo != null && !touristInfo.IsDeleted)
            {
                touristInfo.Name = updatedTouristInfo.Name;
                touristInfo.Email = updatedTouristInfo.Email;
                touristInfo.Interests = updatedTouristInfo.Interests;
                touristInfo.Preferences = updatedTouristInfo.Preferences;
                touristInfo.Grade = updatedTouristInfo.Grade;
                touristInfo.Credit = updatedTouristInfo.Credit;
                touristInfo.Language = updatedTouristInfo.Language;
                touristInfo.UpdatedAt = updatedTouristInfo.UpdatedAt;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var touristInfo = await _context.TouristInfos
                .Where(t => t.Id == id && !t.IsDeleted)
                .FirstOrDefaultAsync();
            if (touristInfo != null)
            {
                touristInfo.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
