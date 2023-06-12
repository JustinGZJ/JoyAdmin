using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Opc.Ua;

namespace JoyAdmin.Application.OPC;

public class OPCAlarmMonitor : BackgroundService
{
    private readonly IOpcUaClientWrapper _opcUaClient;
    private readonly IConfiguration _configuration;
    private readonly IOpcMonitorDataStorage _dataStorage = new OpcMonitorDataStorage();
    private readonly ILogger<OPCAlarmMonitor> _logger;
    private readonly IRepository<Core.Entities.Storage.AlarmHistory> _alarmHistoryRepository;
    private IRepository<Core.Entities.Storage.AlarmHistory> repository;

    public OPCAlarmMonitor(IOpcUaClientWrapper opcUaClient, IConfiguration configuration,
        IServiceScopeFactory serviceScopeFactory, ILogger<OPCAlarmMonitor> logger)
    {
        _opcUaClient = opcUaClient;
        _configuration = configuration;

        _logger = logger;
        var scope = serviceScopeFactory.CreateScope();
        repository = Db.GetRepository<Core.Entities.Storage.AlarmHistory>(scope.ServiceProvider);
        _dataStorage = scope.ServiceProvider.GetService<IOpcMonitorDataStorage>();
        _opcUaClient.OpenConnectOfAnonymous("opc.tcp://127.0.0.1:49320");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var alarmgroups = _configuration.GetSection("PLCAlarmGroups").Get<string[]>();
        var nodeIdList = alarmgroups.Select(nodeId =>
            _opcUaClient.GetAllRelationNodeOfNodeId(nodeId)
                .Select(x => new NodeId(x.NodeId.ToString()))).ToList();
        while (!stoppingToken.IsCancellationRequested)
        {
            var taskFactory = new TaskFactory(TaskScheduler.Current);
            await taskFactory.StartNew(async () =>
            {
                // 你的业务代码写到这里面

                _logger.LogInformation("Worker running at: {time}", DateTime.Now);


                var tasks = nodeIdList.Select(async nodeids =>
                {
                    var dataValues = await _opcUaClient.GetBatchNodeDatasOfAsync(nodeids.ToList());
                    var data = dataValues.Where(x =>
                            x.Value.WrappedValue.TypeInfo.BuiltInType == BuiltInType.Boolean)
                        .ToDictionary(x => x.Key, x => x.Value);
                    foreach (var d in data)
                    {
                        _dataStorage.Update(d.Key, d.Value.Value);
                    }
                });
                await Task.WhenAll(tasks);

                var alarms = _dataStorage.GetAll()
                    .Where(x => x.ValueChanged)
                    .Where(x => !(bool) x.CurrentValue).Select(x => new Core.Entities.Storage.AlarmHistory
                    {
                        Station = x.Name.Split(".")[1],
                        Address = "",
                        Message = x.Name.Split(".")[3],
                        StartTime = x.LastUpdateTime,
                        EndTime = x.UpdateTime
                    }).ToList();

                if (alarms.Any())
                {
                    await repository.InsertNowAsync(alarms, stoppingToken);
                }

                await Task.CompletedTask;
            }, stoppingToken);

            await Task.Delay(500, stoppingToken);
        }
    }
}