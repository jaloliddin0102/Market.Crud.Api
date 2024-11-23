
using Market.Dtos.ProductDetailDto;
using Market.Entities;
using Market.Services;
using Microsoft.AspNetCore.Mvc;  

namespace Crud.Api.Controllers;  

[ApiController]  
[Route("api/[controller]")]  
public class ProductDetailController(IProductDetailService productDetailService, ILogger<ProductDetailController> logger) : ControllerBase  
{  
    

    [HttpGet("{productId}")]  
    public async Task<IActionResult> GetProductDetail(Guid productId)  
    {  
        logger.LogInformation("{ProductId} idli product detallari olinmoqda", productId);  

        var detail = await productDetailService.GetDetailsByProductIdAsync(productId);  
        if (detail == null)  
        {  
            logger.LogWarning("{ProductId} idli product detail topilmadi", productId);  
            return NotFound();  
        }  

        var result = new ProductDetailReadDto  
        {  
            Id = detail.Id,  
            Description = detail.Description,  
            Color = detail.Color,  
            Material = detail.Material,  
            Weight = detail.Weight,  
            QuantityInStock = detail.QuantityInStock,  
            ManufactureDate = detail.ManufactureDate,  
            ExpiryDate = detail.ExpiryDate,  
            Size = detail.Size,  
            Manufacturer = detail.Manufacturer,  
            CountryOfOrigin = detail.CountryOfOrigin  
        };  

        return Ok(result);  
    }  

    [HttpPost("{productId}")]  
    public async Task<IActionResult> CreateProductDetail(Guid productId, [FromBody] ProductDetailCreateDto createDto)  
    {  
        logger.LogInformation("{ProductId} idli product uchun product detail yaratilmoqda", productId);  

        var detail = new ProductDetail  
        {  
            Description = createDto.Description,  
            Color = createDto.Color,  
            Material = createDto.Material,  
            Weight = createDto.Weight,  
            QuantityInStock = createDto.QuantityInStock,  
            ManufactureDate = createDto.ManufactureDate,  
            ExpiryDate = createDto.ExpiryDate,  
            Size = createDto.Size,  
            Manufacturer = createDto.Manufacturer,  
            CountryOfOrigin = createDto.CountryOfOrigin  
        };  

        var createdDetail = await productDetailService.CreateProductDetailAsync(productId, detail);  

        return CreatedAtAction(nameof(GetProductDetail), new { productId = createdDetail.ProductId }, createdDetail);  
    }  

    [HttpPut("{productId}")]  
    public async Task<IActionResult> UpdateProductDetail(Guid productId, [FromBody] ProductDetailUpdateDto updateDto)  
    {  
        logger.LogInformation("{ProductId} idli productni product detaillari o'zgartirilmoqda", productId);  

        var detail = new ProductDetail  
        {  
            Description = updateDto.Description,  
            Color = updateDto.Color,  
            Material = updateDto.Material,  
            Weight = updateDto.Weight,  
            QuantityInStock = updateDto.QuantityInStock,  
            ManufactureDate = updateDto.ManufactureDate,  
            ExpiryDate = updateDto.ExpiryDate,  
            Size = updateDto.Size,  
            Manufacturer = updateDto.Manufacturer,  
            CountryOfOrigin = updateDto.CountryOfOrigin  
        };  

        var updatedDetail = await productDetailService.UpdateProductDetailAsync(productId, detail);  

        if (updatedDetail == null)  
        {  
            logger.LogWarning("{ProductId} idli product detail topilmadi", productId);  
            return NotFound();  
        }  

        return Ok(updatedDetail);  
    }  

    [HttpDelete("{productId}")]  
    public async Task<IActionResult> DeleteProductDetail(Guid productId)  
    {  
        logger.LogInformation("{ProductId} idli product detail o'chirilmoqda", productId);  

        var isDeleted = await productDetailService.DeleteProductDetailAsync(productId);  
        if (!isDeleted)  
        {  
            logger.LogWarning("{ProductId}idli product o'chirish uchun topilmadi.", productId);  
            return NotFound();  
        }  

        return NoContent();  
    }  
}