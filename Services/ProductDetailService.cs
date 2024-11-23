using Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace Market.Services;
public class ProductDetailService : IProductDetailService
{
    private readonly AppDbContext dbContext;

    public ProductDetailService(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<ProductDetail> GetDetailsByProductIdAsync(Guid productId)
    {
        return await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);
    }

    public async Task<ProductDetail> CreateProductDetailAsync(Guid productId, ProductDetail detail)
    {
        detail.Id = Guid.NewGuid();
        detail.ProductId = productId;

        dbContext.ProductDetails.Add(detail);
        await dbContext.SaveChangesAsync();
        return detail;
    }

    public async Task<ProductDetail> UpdateProductDetailAsync(Guid productId, ProductDetail detail)
    {
        var existingDetail = await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);
        if (existingDetail == null) return null;

        existingDetail.Description = detail.Description;
        existingDetail.Color = detail.Color;
        existingDetail.Material = detail.Material;
        existingDetail.Weight = detail.Weight;
        existingDetail.QuantityInStock = detail.QuantityInStock;
        existingDetail.ManufactureDate = detail.ManufactureDate;
        existingDetail.ExpiryDate = detail.ExpiryDate;
        existingDetail.Size = detail.Size;
        existingDetail.Manufacturer = detail.Manufacturer;
        existingDetail.CountryOfOrigin = detail.CountryOfOrigin;

        await dbContext.SaveChangesAsync();
        return existingDetail;
    }

    public async Task<bool> DeleteProductDetailAsync(Guid productId)
    {
        var detail = await dbContext.ProductDetails.FirstOrDefaultAsync(d => d.ProductId == productId);
        if (detail == null) return false;

        dbContext.ProductDetails.Remove(detail);
        await dbContext.SaveChangesAsync();
        return true;
    }
}