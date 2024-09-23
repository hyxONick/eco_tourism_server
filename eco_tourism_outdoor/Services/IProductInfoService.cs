using Microsoft.EntityFrameworkCore;
using eco_tourism_outdoor.Models;
using eco_tourism_outdoor.DB;

namespace eco_tourism_outdoor.Services
{
    public interface IProductInfoService
    {
        Task<List<ProductInfo>> GetAllAsync();
        Task<ProductInfo?> GetByIdAsync(int id);
        Task CreateAsync(ProductInfo newProductInfo);
        Task UpdateAsync(int id, ProductInfo updatedProductInfo);
        Task DeleteAsync(int id);
        Task<List<ProductInfo>> SearchAsync(string searchTerm);
    }

    public class ProductInfoService : IProductInfoService
    {
        private readonly EcoTourismOutdoorContext _context;

        public ProductInfoService(EcoTourismOutdoorContext context)
        {
            _context = context;
        }

        public async Task<List<ProductInfo>> GetAllAsync()
        {
            return await _context.ProductInfo
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProductInfo?> GetByIdAsync(int id)
        {
            return await _context.ProductInfo
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(ProductInfo newProductInfo)
        {
            _context.ProductInfo.Add(newProductInfo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ProductInfo updatedProductInfo)
        {
            var existingProductInfo = await _context.ProductInfo
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingProductInfo != null)
            {
                existingProductInfo.Name = updatedProductInfo.Name;
                existingProductInfo.ProductType = updatedProductInfo.ProductType;
                existingProductInfo.Description = updatedProductInfo.Description;
                existingProductInfo.Price = updatedProductInfo.Price;
                existingProductInfo.NextPrice = updatedProductInfo.NextPrice;
                existingProductInfo.RentalPrice = updatedProductInfo.RentalPrice;
                existingProductInfo.UpdatedAt = DateTime.Now;
                existingProductInfo.IsDeleted = updatedProductInfo.IsDeleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var productInfo = await _context.ProductInfo
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (productInfo != null)
            {
                productInfo.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductInfo>> SearchAsync(string searchTerm)
        {
            return await _context.ProductInfo
                .Where(p => !p.IsDeleted && 
                            (p.Name.Contains(searchTerm) || 
                            p.ProductType.Contains(searchTerm) || 
                            (p.Description ?? string.Empty).Contains(searchTerm)))
                .ToListAsync();
        }
    }
}