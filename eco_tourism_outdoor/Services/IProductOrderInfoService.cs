using Microsoft.EntityFrameworkCore;
using eco_tourism_outdoor.Models;
using eco_tourism_outdoor.DB;

namespace eco_tourism_outdoor.Services
{
    public interface IProductOrderInfoService
    {
        Task<List<ProductOrderInfo>> GetAllAsync();
        Task<ProductOrderInfo?> GetByIdAsync(int id);
        Task<List<ProductOrderInfo>> GetByBuyerIdAsync(int id);
        Task CreateAsync(ProductOrderInfo newProductOrderInfo);
        Task UpdateAsync(int id, ProductOrderInfo updatedProductOrderInfo);
        Task DeleteAsync(int id);
    }

    public class ProductOrderInfoService : IProductOrderInfoService
    {
        private readonly EcoTourismOutdoorContext _context;

        public ProductOrderInfoService(EcoTourismOutdoorContext context)
        {
            _context = context;
        }

        public async Task<List<ProductOrderInfo>> GetAllAsync()
        {
            return await _context.ProductOrderInfo
                .Where(p => !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<ProductOrderInfo?> GetByIdAsync(int id)
        {
            return await _context.ProductOrderInfo
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ProductOrderInfo>> GetByBuyerIdAsync(int id)
        {
            return await _context.ProductOrderInfo
                .Where(p => p.BuyerId == id && !p.IsDeleted)
                .ToListAsync();
        }

        public async Task CreateAsync(ProductOrderInfo newProductOrderInfo)
        {
            _context.ProductOrderInfo.Add(newProductOrderInfo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, ProductOrderInfo updatedProductOrderInfo)
        {
            var existingProductOrderInfo = await _context.ProductOrderInfo
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (existingProductOrderInfo != null)
            {
                existingProductOrderInfo.BuyerId = updatedProductOrderInfo.BuyerId;
                existingProductOrderInfo.ProductId = updatedProductOrderInfo.ProductId;
                existingProductOrderInfo.Memo = updatedProductOrderInfo.Memo;
                existingProductOrderInfo.TotalAmount = updatedProductOrderInfo.TotalAmount;
                existingProductOrderInfo.NextAmount = updatedProductOrderInfo.NextAmount;
                existingProductOrderInfo.UpdatedAt = DateTime.Now;
                existingProductOrderInfo.IsDeleted = updatedProductOrderInfo.IsDeleted;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var productOrderInfo = await _context.ProductOrderInfo
                .Where(p => p.Id == id && !p.IsDeleted)
                .FirstOrDefaultAsync();

            if (productOrderInfo != null)
            {
                productOrderInfo.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
