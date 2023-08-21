using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class DesktopMenu:ServiceBase<Base_DesktopMenu>
{
    public DesktopMenu(IRepository<Base_DesktopMenu> repository) : base(repository)
    {
    }
}