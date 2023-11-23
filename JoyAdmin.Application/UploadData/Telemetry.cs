using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DynamicApiController;
using Furion.EventBus;
using JoyAdmin.Core.Entities.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace JoyAdmin.Application.UploadData;

public class TelemetryDataService:ServiceBase<TelemetryData>
{
    public TelemetryDataService(IRepository<TelemetryData> repository) : base(repository)
    {
    }
    
    
}

public class TelemetryService:ControllerBase
{
    private readonly IEventPublisher _eventPublisher;

    public TelemetryService(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }
    [HttpPost("/api/telemetry/upload")]
    public async Task Upload()
    {
        // get http request body
        var body= await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
       var data =JObject.Parse(body);
        
        // publish event
        await _eventPublisher.PublishAsync("Telemetry:Upload", data);
    }
}

public class TelemetryLatestService : ServiceBase<TelemetryLatest>
{
    public TelemetryLatestService(IRepository<TelemetryLatest> repository) : base(repository)
    {
    }
}