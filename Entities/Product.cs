using System.ComponentModel.DataAnnotations.Schema;

namespace Market.Entities;
public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal Price { get; set; }
    
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    public EProductStatus Status { get; set; }

    // faqatgina navigatsiya uchun ishlatiladi
    public ProductDetail? ProductDetail { get; set; }
}

public enum EProductStatus
{
    Inactive,
    Active,
    Soldout
}