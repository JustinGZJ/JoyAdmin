using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class Notice:ServiceBase<Base_Notice>
{
    public Notice(IRepository<Base_Notice> repository) : base(repository)
    {
    }
}