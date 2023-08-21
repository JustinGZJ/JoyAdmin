using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class ProcessListService : ServiceBase<Base_ProcessList>
{
    public ProcessListService(IRepository<Base_ProcessList> repository) : base(repository)
    {
    }
}