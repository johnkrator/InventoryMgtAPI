using AutoMapper;
using InventoryMgtApp.BLL.Services.Contracts;
using InventoryMgtApp.DAL.Entities.DTOs.Requests;
using InventoryMgtApp.DAL.Entities.DTOs.Responses;
using InventoryMgtApp.DAL.Entities.Models;
using InventoryMgtApp.DAL.Exceptions;
using InventoryMgtApp.DAL.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace InventoryMgtApp.BLL.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;
    private readonly IRepository<Product> _productRepository;

    public ProductService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _productRepository = _unitOfWork.GetRepository<Product>();
    }

    public async Task<Status> CreateNewProduct(ProductRequestDto productRequestDto)
    {
        var status = new Status();
        var userExist = await _userManager.FindByIdAsync(productRequestDto.AppUserId.ToString());

        if (userExist is null)
        {
            status.StatusCode = 0;
            status.Message = $"User id: {productRequestDto.AppUserId} does not match with the product";

            return status;
        }

        var newProduct = _mapper.Map<Product>(productRequestDto);
        var createdProduct = await _productRepository.AddAsync(newProduct);

        if (createdProduct is not null)
        {
            var result = _mapper.Map<ProductRequestDto>(createdProduct);
            if (result is not null)
            {
                status.StatusCode = 1;
                status.Message = "Product was added successfully";

                return status;
            }
        }

        status.StatusCode = 0;
        status.Message = "Something went wrong. Was unable to add product";

        return status;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllUserProducts(Guid id)
    {
        var userExist = await _userManager.FindByIdAsync(id.ToString());

        if (userExist is null)
            throw new NotFoundException("User not found");

        var products = _productRepository.GetQueryable(x => x.AppUserId == id.ToString());

        if (products is null)
            throw new NotFoundException("Product not found");

        var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);

        return result;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetProducts()
    {
        var products = await _productRepository.GetAllAsync();
        var result = _mapper.Map<IEnumerable<ProductResponseDto>>(products);

        return result;
    }

    public async Task<ProductResponseDto> GetProduct(Guid id)
    {
        try
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                throw new NotFoundException("Product not found");

            var result = _mapper.Map<ProductResponseDto>(product);

            return result;
        }
        catch (NotFoundException e)
        {
            throw new NotFoundException(e.Message);
        }
    }

    public async Task<Status> UpdateProduct(Guid productId, ProductRequestDto productRequestDto)
    {
        var status = new Status();

        var productToUpdate = await _productRepository.GetByIdAsync(productId);

        if (productToUpdate is null)
        {
            status.StatusCode = 0;
            status.Message = $"Product with id: {productId} not found.";

            return status;
        }

        // Check if user has permission to update this product
        var userExist = await _userManager.FindByIdAsync(productRequestDto.AppUserId.ToString());

        if (userExist is null)
        {
            status.StatusCode = 0;
            status.Message = $"User id: {productRequestDto.AppUserId} does not match with the product";

            return status;
        }

        // Update the product with the new information
        _mapper.Map(productRequestDto, productToUpdate);

        var updatedProduct = await _productRepository.UpdateAsync(productToUpdate);

        if (updatedProduct is not null)
        {
            var result = _mapper.Map<ProductRequestDto>(updatedProduct);
            if (result is not null)
            {
                status.StatusCode = 1;
                status.Message = "Product was updated successfully";

                return status;
            }
        }

        status.StatusCode = 0;
        status.Message = "Something went wrong. Was unable to update product";

        return status;
    }

    public async Task<Status> DeleteProduct(Guid productId)
    {
        var status = new Status();
        var product = await _productRepository.GetByIdAsync(productId);

        if (product is null)
        {
            status.StatusCode = 0;
            status.Message = $"Product with Id {productId} not found";
            return status;
        }

        try
        {
            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();

            status.StatusCode = 1;
            status.Message = $"Product with Id {productId} was deleted successfully";
        }
        catch (Exception ex)
        {
            status.StatusCode = 0;
            status.Message = $"An error occurred while deleting product with Id {productId}: {ex.Message}";
        }

        return status;
    }

    public async Task<Status> ProductImageUpload(string id, IFormFile file)
    {
        var status = new Status();
        var product = await _productRepository.GetSingleByAsync(x => x.ProductId.ToString() == id);

        if (product is null)
        {
            status.StatusCode = 0;
            status.Message = $"Product with id {id} was not found";

            return status;
        }

        string path;

        if (file.Length > 0)
        {
            path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, "UploadedFiles"));
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            await using var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create);
            await file.CopyToAsync(fileStream);

            product.ProductImagePath = path + $"/{file.FileName}";

            var updatedProduct = await _productRepository.UpdateAsync(product);

            if (updatedProduct is null)
            {
                status.StatusCode = 0;
                status.Message = "Unable to upload image";

                return status;
            }

            status.StatusCode = 1;
            status.Message = $"File {file.FileName} was uploaded successfully. Path: {path}";

            return status;
        }

        status.StatusCode = 0;
        status.Message = "Invalid file size";

        return status;
    }
}
