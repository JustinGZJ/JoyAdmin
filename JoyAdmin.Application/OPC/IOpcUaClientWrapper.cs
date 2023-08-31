using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Opc.Ua;
using Opc.Ua.Client;
using OpcUaHelper;

namespace JoyAdmin.Application.OPC;

public interface IOpcUaClientWrapper
{
    bool ConnectStatus { get; }

    void BatchNodeIdDatasSubscription(string key, string[] nodeIds,
        Action<string, MonitoredItem, MonitoredItemNotificationEventArgs> callback);

    bool BatchWriteNodeIds(string[] nodeIdArray, object[] nodeIdValueArray);
    bool CancelAllNodeIdDatasSubscription();
    bool CancelSingleNodeIdDatasSubscription(string key);
    void CloseConnect();
    ReferenceDescription[] GetAllRelationNodeOfNodeId(string nodeId);
    Dictionary<string, DataValue> GetAllRelationNodeValue(string nodeId);
    Task<Dictionary<string, DataValue>> GetBatchNodeDatasOfAsync(List<NodeId> nodeIdList);
    Dictionary<string, DataValue> GetBatchNodeDatasOfSync(List<NodeId> nodeIdList);
    OpcNodeAttribute[] GetCurrentNodeAttributes(string nodeId);
    DataValue GetCurrentNodeValue(string nodeId);
    T GetCurrentNodeValue<T>(string nodeId);
    Task<T> GetCurrentNodeValueOfAsync<T>(string nodeId);
    void OpenConnectOfAccount(string serverUrl, string userName, string userPwd);
    void OpenConnectOfAnonymous(string serverUrl);
    void OpenConnectOfCertificate(string serverUrl, string certificatePath, string secreKey);
    List<DataValue> ReadSingleNodeIdHistoryDatas(string nodeId, DateTime startTime, DateTime endTime);
    List<T> ReadSingleNodeIdHistoryDatas<T>(string nodeId, DateTime startTime, DateTime endTime);

    void SingleNodeIdDatasSubscription(string key, string nodeId,
        Action<string, MonitoredItem, MonitoredItemNotificationEventArgs> callback);

    Task<bool> WriteSingleNodeIdOfAsync<T>(string nodeId, T value);
    bool WriteSingleNodeIdOfSync<T>(string nodeId, T value);
}