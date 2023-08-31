using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class Product : ServiceBase<Base_Product>
{
    public Product(IRepository<Base_Product> repository) : base(repository)
    {
    }
}