using Microsoft.EntityFrameworkCore;
using eco_tourism_outdoor.Models;
using eco_tourism_outdoor.DB;

namespace eco_tourism_outdoor.Services
{
    public interface IEquipmentRentalService
    {
        Task<List<EquipmentRental>> GetAllAsync();
        Task<EquipmentRental?> GetByIdAsync(int id);
        Task<List<EquipmentRental>> GetByRenterIdAsync(int id);
        Task CreateAsync(EquipmentRental newEquipmentRental);
        Task UpdateAsync(int id, EquipmentRental updatedEquipmentRental);
        Task DeleteAsync(int id);
    }

    public class EquipmentRentalService : IEquipmentRentalService
    {
        private readonly EcoTourismOutdoorContext _context;

        public EquipmentRentalService(EcoTourismOutdoorContext context)
        {
            _context = context;
        }

        public async Task<List<EquipmentRental>> GetAllAsync()
        {
            return await _context.EquipmentRental
                .Where(e => !e.IsDeleted)
                .ToListAsync();
        }

        public async Task<EquipmentRental?> GetByIdAsync(int id)
        {
            return await _context.EquipmentRental
                .Where(e => e.Id == id && !e.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<List<EquipmentRental>> GetByRenterIdAsync(int id)
        {
            return await _context.EquipmentRental
                .Where(e => e.RenterId == id && !e.IsDeleted)
                .ToListAsync();
        }

        public async Task CreateAsync(EquipmentRental newEquipmentRental)
        {
            _context.EquipmentRental.Add(newEquipmentRental);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EquipmentRental updatedEquipmentRental)
        {
            var existingEquipmentRental = await _context.EquipmentRental
                .Where(e => e.Id == id && !e.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingEquipmentRental != null)
            {
                existingEquipmentRental.RenterId = updatedEquipmentRental.RenterId;
                existingEquipmentRental.ProductId = updatedEquipmentRental.ProductId;
                existingEquipmentRental.EquipmentType = updatedEquipmentRental.EquipmentType;
                existingEquipmentRental.RentalDate = updatedEquipmentRental.RentalDate;
                existingEquipmentRental.ReturnDate = updatedEquipmentRental.ReturnDate;
                existingEquipmentRental.Status = updatedEquipmentRental.Status;
                existingEquipmentRental.UpdatedAt = DateTime.Now;
                existingEquipmentRental.IsDeleted = updatedEquipmentRental.IsDeleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var equipmentRental = await _context.EquipmentRental
                .Where(e => e.Id == id && !e.IsDeleted)
                .FirstOrDefaultAsync();

            if (equipmentRental != null)
            {
                equipmentRental.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
