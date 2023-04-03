using InventoryMgtApp.BLL.Services.Contracts;
using InventoryMgtApp.DAL.Entities.DTOs.Requests;
using InventoryMgtApp.DAL.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace InventoryMgtApp.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("CreateNewProduct")]
    public async Task<ActionResult> CreateNewProduct(ProductRequestDto productRequestDto)
    {
        var newProduct = await _productService.CreateNewProduct(productRequestDto);

        if (newProduct is null)
            return BadRequest();

        return Ok(newProduct);
    }

    [HttpGet("GetAllUserProducts")]
    public async Task<ActionResult> GetAllUserProducts(Guid id)
    {
        var getUserProducts = await _productService.GetAllUserProducts(id);

        if (getUserProducts is null)
            return NotFound();

        return Ok(getUserProducts);
    }

    [HttpGet("GetProducts")]
    public async Task<IActionResult> GetProducts()
    {
        var getProducts = await _productService.GetProducts();

        if (getProducts is null)
            return NotFound();

        return Ok(getProducts);
    }

    [HttpGet("GetProduct")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var getProduct = await _productService.GetProduct(id);

        if (getProduct is null)
            return NotFound();

        return Ok(getProduct);
    }

    [HttpPut("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(Guid productId, ProductRequestDto productRequestDto)
    {
        var updateProduct = await _productService.UpdateProduct(productId, productRequestDto);

        if (updateProduct is null)
            return BadRequest();

        return Ok(updateProduct);
    }

    [HttpDelete("DeleteProduct")]
    public async Task<ActionResult> DeleteProduct(Guid productId)
    {
        var status = new Status();
        var deleteProduct = await _productService.DeleteProduct(productId);

        if (deleteProduct is null)
            return BadRequest();

        return NoContent();
    }

    [HttpPost("ProductImageUpload")]
    public async Task<IActionResult> ProductImageUpload(string id, IFormFile file)
    {
        var uploadProductImage = await _productService.ProductImageUpload(id, file);

        if (uploadProductImage is null)
            return BadRequest();

        return Ok(uploadProductImage);
    }
}
