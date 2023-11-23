using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class ProcessService : ServiceBase<Base_Process>
{
    public ProcessService(IRepository<Base_Process> repository) : base(repository)
    {
    }
}

public class ProcessListService : ServiceBase<BaseProcessList>
{
    public ProcessListService(IRepository<BaseProcessList> repository) : base(repository)
    {
    }
}