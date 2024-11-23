using Market.Entities;

namespace Market.Dtos.ProductDto;
public class ProductUpdateDto
{
    public string? Name { get; set; }
    public decimal Price { get; set; }
    public EProductStatus Status { get; set; }
}