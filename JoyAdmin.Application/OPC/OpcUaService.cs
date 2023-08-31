using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Opc.Ua;
using OpcUaHelper;

namespace JoyAdmin.Application.OPC;

public class OpcUaService : IDynamicApiController
{
    private readonly IConfiguration _configuration;
    private readonly IOpcMonitorDataStorage _dataStorage;
    private readonly IOpcUaClientWrapper _opcUaClient;

    public OpcUaService(IOpcUaClientWrapper opcUaClient, IOpcMonitorDataStorage dataStorage,
        IConfiguration configuration)
    {
        _opcUaClient = opcUaClient;
        _dataStorage = dataStorage;
        _configuration = configuration;
        _opcUaClient.OpenConnectOfAnonymous("opc.tcp://127.0.0.1:49320");
        // _opcUaClient.GetAllRelationNodeOfNodeId()
    }

    /// <summary>
    ///     连接OPC 服务器
    /// </summary>
    /// <param name="serverUrl"></param>
    [AllowAnonymous]
    public void OpenConnect(string serverUrl)
    {
        _opcUaClient.OpenConnectOfAnonymous(serverUrl);
    }

    /// <summary>
    ///     读取单节点历史记录
    /// </summary>
    /// <param name="nodeId"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    [AllowAnonymous]
    public void ReadSingleNodeIdHistoryDatas(string nodeId, DateTime startTime, DateTime endTime)
    {
        _opcUaClient.ReadSingleNodeIdHistoryDatas(nodeId, startTime, endTime);
    }

    /// <summary>
    ///     读取当前节点的值
    /// </summary>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public object GetCurrentNodeValue(string nodeId)
    {
        return _opcUaClient.GetCurrentNodeValue(nodeId).Value;
    }

    /// <summary>
    ///     读取当前节点的属性
    /// </summary>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public OpcNodeAttribute[] GetCurrentNodeAttributes(string nodeId)
    {
        return _opcUaClient.GetCurrentNodeAttributes(nodeId);
    }

    /// <summary>
    ///     读取目录下所有的节点的数据
    /// </summary>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public Dictionary<string, object> GetAllRelationNodeValue(string nodeId)
    {
        return _opcUaClient.GetAllRelationNodeValue(nodeId).ToDictionary(x => x.Key, x => x.Value.Value);
    }

    /// <summary>
    ///     浏览节点下的所有节点信息
    /// </summary>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public ReferenceDescription[] GetAllRelationNodeOfNodeId(string nodeId)
    {
        var referenceDescriptions = _opcUaClient.GetAllRelationNodeOfNodeId(nodeId);
        return referenceDescriptions;
    }

    /// <summary>
    ///     写入单个节点
    /// </summary>
    /// <param name="nodeId"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public Task<bool> WriteSingleNodeIdOfAsync(string nodeId, object data)
    {
        return _opcUaClient.WriteSingleNodeIdOfAsync(nodeId, data);
    }

    /// <summary>
    ///     监听一个组
    /// </summary>
    /// <param name="nodeId"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public IEnumerable<string> MonitAllRelationNodeOfNodeId(string nodeId)
    {
        var referenceDescriptions = _opcUaClient.GetAllRelationNodeOfNodeId(nodeId);
        var nodeids = referenceDescriptions.Select(x => x.NodeId.ToString()).ToArray();
        _opcUaClient.BatchNodeIdDatasSubscription(nodeId, nodeids, (s, item, arg3) =>
        {
            var notification = arg3.NotificationValue as MonitoredItemNotification;
            _dataStorage.Update(item.DisplayName, notification?.Value.Value);
        });
        return nodeids;
    }

    /// <summary>
    ///     获取监听到的OPC数据
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<Dictionary<string, object>> GetCurrentAlarm()
    {
        var alarmgroups = _configuration.GetSection("PLCAlarmGroups").Get<string[]>();
        var nodeIdList = alarmgroups.SelectMany(nodeId =>
            _opcUaClient.GetAllRelationNodeOfNodeId(nodeId)
                .Select(x => new NodeId(x.NodeId.ToString()))).ToList();
        var dataValues = await _opcUaClient.GetBatchNodeDatasOfAsync(nodeIdList);
        var data = dataValues.Where(x =>
                x.Value.WrappedValue.TypeInfo.BuiltInType == BuiltInType.Boolean)
            .Where(x => x.Value.Value is bool and true)
            .ToDictionary(x => x.Key, x => x.Value.Value);
        return data;
    }
}