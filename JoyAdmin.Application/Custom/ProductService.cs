using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using JoyAdmin.Core.Entities.Custom;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Custom;


public class ProductService : CrudBaseService<Base_Product>
{
    public ProductService(IRepository<Base_Product> repository) : base(repository)
    {
    }
}