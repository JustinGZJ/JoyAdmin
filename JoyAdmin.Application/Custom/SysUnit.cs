using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class SysUnit : ServiceBase<Sys_Unit>
{
    public SysUnit(IRepository<Sys_Unit> repository) : base(repository)
    {
    }
}