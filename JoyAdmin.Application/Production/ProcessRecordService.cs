using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Production;

namespace JoyAdmin.Application.Production;

public class ProcessRecordService : ServiceBase<Production_ProcessRecord>
{
    public ProcessRecordService(IRepository<Production_ProcessRecord> repository) : base(repository)
    {
    }
}