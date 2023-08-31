using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;

namespace JoyAdmin.Application.Custom;

public class ProcessLine : ServiceBase<Base_ProcessLine>
{
    public ProcessLine(IRepository<Base_ProcessLine> repository) : base(repository)
    {
    }
}

public class ProcessLineList : ServiceBase<Base_ProcessLineList>
{
    public ProcessLineList(IRepository<Base_ProcessLineList> repository) : base(repository)
    {
    }
}