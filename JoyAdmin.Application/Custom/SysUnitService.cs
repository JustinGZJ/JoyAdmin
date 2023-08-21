using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class SysUnitService : CrudBaseService<Sys_Unit>
{
    public SysUnitService(IRepository<Sys_Unit> repository) : base(repository)
    {
    }
}