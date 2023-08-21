using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Custom;

public class ProcessLine:ServiceBase<Base_ProcessLine>
{
    public ProcessLine(IRepository<Base_ProcessLine> repository) : base(repository)
    {
    }
}

public class ProcessLineList:ServiceBase<Base_ProcessLineList>
{
    public ProcessLineList(IRepository<Base_ProcessLineList> repository) : base(repository)
    {
    }
}