using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Custom;
using Microsoft.EntityFrameworkCore;

namespace JoyAdmin.Application.Production;

public static class ProcessLineQueryExtension
{
    public static async Task GetProcessFromProcessLineAsync(this Base_ProcessLine processLine,
        List<Base_Process> processList)
    {
        //  queryable.Include(x=>)
        var data = await Db.GetRepository<BaseProcessLineList>().Entities
            .Where(x => x.ProcessLine_Id == processLine.ProcessLine_Id)
            .OrderBy(x => x.Sequence)
            .Include(x => x.Process)
            .Include(x => x.ProcessLineDown)
            .ToListAsync();
        foreach (var processLineList in data)
        {
            switch (processLineList.ProcessLineType)
            {
                case "工艺路线":
                    await GetProcessFromProcessLineAsync(processLineList.ProcessLineDown, processList);
                    break;
                case "工序":
                    processList.Add(processLineList.Process);
                    break;
            }
        }
    }
    
}