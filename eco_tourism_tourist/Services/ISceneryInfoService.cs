using Microsoft.EntityFrameworkCore;
using eco_tourism_tourist.Models;
using eco_tourism_tourist.DB;

namespace eco_tourism_tourist.Services
{
    public interface ISceneryInfoService
    {
        Task<List<SceneryInfo>> GetAllAsync();
        Task<SceneryInfo?> GetByIdAsync(int id);
        Task CreateAsync(SceneryInfo newSceneryInfo);
        Task UpdateAsync(int id, SceneryInfo updatedSceneryInfo);
        Task DeleteAsync(int id);
        Task<List<SceneryInfo>> SearchAsync(string searchTerm);
    }
public class SceneryInfoService(EcoTourismTouristContext context) : ISceneryInfoService
{
    private readonly EcoTourismTouristContext _context = context;

    public async Task<List<SceneryInfo>> GetAllAsync()
    {
        return await _context.SceneryInfos
            .Where(s => !s.IsDeleted)
            .ToListAsync();
    }

    public async Task<SceneryInfo?> GetByIdAsync(int id)
    {
        return await _context.SceneryInfos
            .Where(s => s.Id == id && !s.IsDeleted)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(SceneryInfo newSceneryInfo)
    {
        _context.SceneryInfos.Add(newSceneryInfo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, SceneryInfo updatedSceneryInfo)
    {
        var existingSceneryInfo = await _context.SceneryInfos
            .Where(s => s.Id == id && !s.IsDeleted)
            .FirstOrDefaultAsync();

        if (existingSceneryInfo != null)
        {
            existingSceneryInfo.Name = updatedSceneryInfo.Name;
            existingSceneryInfo.Address = updatedSceneryInfo.Address;
            existingSceneryInfo.Description = updatedSceneryInfo.Description;
            existingSceneryInfo.Type = updatedSceneryInfo.Type;
            existingSceneryInfo.PicUrl = updatedSceneryInfo.PicUrl;
            existingSceneryInfo.Longitude = updatedSceneryInfo.Longitude;
            existingSceneryInfo.Latitude = updatedSceneryInfo.Latitude;
            existingSceneryInfo.UpdatedAt = updatedSceneryInfo.UpdatedAt;
            existingSceneryInfo.IsDeleted = updatedSceneryInfo.IsDeleted;

            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var sceneryInfo = await _context.SceneryInfos
            .Where(s => s.Id == id && !s.IsDeleted)
            .FirstOrDefaultAsync();

        if (sceneryInfo != null)
        {
            sceneryInfo.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<SceneryInfo>> SearchAsync(string searchTerm)
    {
        return await _context.SceneryInfos
            .Where(s => !s.IsDeleted && 
                        (s.Name.Contains(searchTerm) || 
                         s.Address.Contains(searchTerm) || 
                        (s.Description ?? string.Empty).Contains(searchTerm)))
            .ToListAsync();
    }
}

}