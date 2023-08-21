using System.Collections.Generic;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Custom;

public class ProcessLineService:CrudBaseService<Base_ProcessLine>
{
    public ProcessLineService(IRepository<Base_ProcessLine> repository) : base(repository)
    {
    }
}

public class ProcessLineListService:CrudBaseService<Base_ProcessLineList>
{
    public ProcessLineListService(IRepository<Base_ProcessLineList> repository) : base(repository)
    {
    }
}