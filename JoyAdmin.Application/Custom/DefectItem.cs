using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class DefectItem:ServiceBase<Base_DefectItem>
{
    public DefectItem(IRepository<Base_DefectItem> repository) : base(repository)
    {
    }
}