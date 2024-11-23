using Market.Entities;
using Microsoft.EntityFrameworkCore;
namespace Market.Services;
public class ProductService(AppDbContext dbContext) : IProductService
{

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await dbContext.Products.Include(p => p.ProductDetail).ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        return await dbContext.Products.Include(p => p.ProductDetail).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        product.Id = Guid.NewGuid();
        product.CreatedAt = DateTime.UtcNow;
        product.ModifiedAt = DateTime.UtcNow;

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<Product> UpdateProductAsync(Guid id, Product product)
    {
        var existingProduct = await dbContext.Products.FindAsync(id);
        if (existingProduct == null) return null;

        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.ModifiedAt = DateTime.UtcNow;
        existingProduct.Status = product.Status;

        await dbContext.SaveChangesAsync();
        return existingProduct;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await dbContext.Products.FindAsync(id);
        if (product == null) return false;

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return true;
    }
}