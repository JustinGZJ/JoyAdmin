using Furion.DatabaseAccessor;
using JoyAdmin.Core.Entities.Storage;

namespace JoyAdmin.Application.UploadData;

public class TelemetryDataService:ServiceBase<TelemetryData>
{
    public TelemetryDataService(IRepository<TelemetryData> repository) : base(repository)
    {
    }
}

public class TelemetryLatestService : ServiceBase<TelemetryLatest>
{
    public TelemetryLatestService(IRepository<TelemetryLatest> repository) : base(repository)
    {
    }
}