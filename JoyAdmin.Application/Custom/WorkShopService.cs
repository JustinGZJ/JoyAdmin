using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class WorkShopService:CrudBaseService<Base_WorkShop>
{
    public WorkShopService(IRepository<Base_WorkShop> repository) : base(repository)
    {
    }
}