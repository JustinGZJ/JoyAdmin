using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class DesktopMenuService:CrudBaseService<Base_DesktopMenu>
{
    public DesktopMenuService(IRepository<Base_DesktopMenu> repository) : base(repository)
    {
    }
}