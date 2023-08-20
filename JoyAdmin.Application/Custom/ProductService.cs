using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Core.Entities.Custom;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Custom;

public class ProductService : IDynamicApiController
{
    private readonly IRepository<Base_Product> _productRepository;

    public ProductService(IRepository<Base_Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Base_Product>> GetProductList()
    {
        var list = await _productRepository.Entities.ToListAsync();
        return list;
    }
    
    public async Task<Base_Product> GetProductById(int id)
    {
        var product = await _productRepository.Entities.FindAsync(id);
        return product;
    }
    
    public  Task AddProduct(Base_Product product)
    {
        return _productRepository.InsertNowAsync(product);
    }
    
    public  Task UpdateProduct(Base_Product product)
    {
        return _productRepository.UpdateNowAsync(product);
    }
    
    public  Task DeleteProduct(int id)
    {
        return _productRepository.DeleteAsync(id);
    }
    
    
}