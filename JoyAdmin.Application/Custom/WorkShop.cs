using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class WorkShop : ServiceBase<Base_WorkShop>
{
    public WorkShop(IRepository<Base_WorkShop> repository) : base(repository)
    {
    }
}