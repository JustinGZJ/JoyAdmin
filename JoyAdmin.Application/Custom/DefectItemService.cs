using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class DefectItemService:CrudBaseService<Base_DefectItem>
{
    public DefectItemService(IRepository<Base_DefectItem> repository) : base(repository)
    {
    }
}