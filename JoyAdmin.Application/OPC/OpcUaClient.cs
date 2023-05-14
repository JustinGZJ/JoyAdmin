﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Opc.Ua;
using Opc.Ua.Client;
using OpcUaHelper;

namespace JoyAdmin.Application.OPC;

public class OpcData
{
    public DateTime UpdateTime { get; set; }= DateTime.Now;
    public DateTime LastUpdateTime { get; set; }= DateTime.Now;
    public object PreviousValue { get; set; } 
    public object CurrentValue { get; set; } 
    public bool ValueChanged { get; set; }
    public string Name { get; set; }
}

public interface IOpcMonitorDataStorage
{
    void Update(string name, object value);
    List<OpcData> GetAll();
    OpcData Get(string name);
}

public class OpcMonitorDataStorage : IOpcMonitorDataStorage,ITransient
{
    private ConcurrentDictionary<string, OpcData> _dictionary = new();

    public OpcMonitorDataStorage()
    {
    }

    public void Update(string name, object value)
    {
        if (!_dictionary.ContainsKey(name))
        {
            _dictionary[name] = new OpcData
            {
                UpdateTime = DateTime.Now,
                LastUpdateTime = DateTime.Now,
                PreviousValue = value,
                CurrentValue = value,
                Name = name
            };
        }
        else
        {
            var data = _dictionary[name];
            if (!_dictionary[name].CurrentValue.Equals(value))
            {
                _dictionary[name].PreviousValue= _dictionary[name].CurrentValue;
                _dictionary[name].CurrentValue = value;
                _dictionary[name].LastUpdateTime = _dictionary[name].UpdateTime;
                _dictionary[name].UpdateTime = DateTime.Now;
                _dictionary[name].ValueChanged = true;
            }
            else
            {
                _dictionary[name].ValueChanged = false;
            }
        }
    }

    public List<OpcData> GetAll()
    {
        return _dictionary.Select(x => x.Value).ToList();
    }
    
    public OpcData Get(string name)
    {
        return _dictionary[name];
    }
}




public class OpcUaClientWrapper : IOpcUaClientWrapper,ISingleton
{
    #region 基础参数
    

    //OPCUA客户端
    private OpcUaClient opcUaClient;

    #endregion

    /// <summary>
    /// 构造函数
    /// </summary>
    public OpcUaClientWrapper()
    {
        opcUaClient = new OpcUaClient();
    }

    /// <summary>
    /// 连接状态
    /// </summary>
    public bool ConnectStatus => opcUaClient.Connected;


    #region 公有方法

    /// <summary>
    /// 打开连接【匿名方式】
    /// </summary>
    /// <param name="serverUrl">服务器URL【格式：opc.tcp://服务器IP地址/服务名称】</param>
    public async void OpenConnectOfAnonymous(string serverUrl)
    {
        if (!string.IsNullOrEmpty(serverUrl))
        {
            try
            {
                opcUaClient.UserIdentity = new UserIdentity(new AnonymousIdentityToken());

                await opcUaClient.ConnectServer(serverUrl);
            }
            catch (Exception ex)
            {
                HandleException("连接失败！！！", ex);
            }
        }
    }

    /// <summary>
    /// 打开连接【账号方式】
    /// </summary>
    /// <param name="serverUrl">服务器URL【格式：opc.tcp://服务器IP地址/服务名称】</param>
    /// <param name="userName">用户名称</param>
    /// <param name="userPwd">用户密码</param>
    public async void OpenConnectOfAccount(string serverUrl, string userName, string userPwd)
    {
        if (!string.IsNullOrEmpty(serverUrl) &&
            !string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(userPwd))
        {
            try
            {
                opcUaClient.UserIdentity = new UserIdentity(userName, userPwd);

                await opcUaClient.ConnectServer(serverUrl);
            }
            catch (Exception ex)
            {
                HandleException("连接失败！！！", ex);
            }
        }
    }

    /// <summary>
    /// 打开连接【证书方式】
    /// </summary>
    /// <param name="serverUrl">服务器URL【格式：opc.tcp://服务器IP地址/服务名称】</param>
    /// <param name="certificatePath">证书路径</param>
    /// <param name="secreKey">密钥</param>
    public async void OpenConnectOfCertificate(string serverUrl, string certificatePath, string secreKey)
    {
        if (!string.IsNullOrEmpty(serverUrl) &&
            !string.IsNullOrEmpty(certificatePath) && !string.IsNullOrEmpty(secreKey))
        {
            try
            {
                X509Certificate2 certificate = new X509Certificate2(certificatePath, secreKey,
                    X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);
                opcUaClient.UserIdentity = new UserIdentity(certificate);

                await opcUaClient.ConnectServer(serverUrl);
            }
            catch (Exception ex)
            {
                HandleException("连接失败！！！", ex);
            }
        }
    }


    /// <summary>
    /// 关闭连接
    /// </summary>
    public void CloseConnect()
    {
        if (opcUaClient != null)
        {
            try
            {
                opcUaClient.Disconnect();
            }
            catch (Exception ex)
            {
                HandleException("关闭连接失败！！！", ex);
            }
        }
    }


    /// <summary>
    /// 获取到当前节点的值【同步读取】
    /// </summary>
    /// <typeparam name="T">节点对应的数据类型</typeparam>
    /// <param name="nodeId">节点</param>
    /// <returns>返回当前节点的值</returns>
    public T GetCurrentNodeValue<T>(string nodeId)
    {
        T value = default(T);
        if (!string.IsNullOrEmpty(nodeId) && ConnectStatus)
        {
            try
            {
                value = opcUaClient.ReadNode<T>(nodeId);
            }
            catch (Exception ex)
            {
                HandleException("读取失败！！！", ex);
            }
        }

        return value;
    }

    /// <summary>
    /// 获取到当前节点数据【同步读取】
    /// </summary>
    /// <typeparam name="T">节点对应的数据类型</typeparam>
    /// <param name="nodeId">节点</param>
    /// <returns>返回当前节点的值</returns>
    public DataValue GetCurrentNodeValue(string nodeId)
    {
        DataValue dataValue = null;
        if (!string.IsNullOrEmpty(nodeId) && ConnectStatus)
        {
            try
            {
                dataValue = opcUaClient.ReadNode(nodeId);
            }
            catch (Exception ex)
            {
                HandleException("读取失败！！！", ex);
            }
        }

        return dataValue;
    }

    /// <summary>
    /// 获取到批量节点数据【同步读取】
    /// </summary>
    /// <param name="nodeIds">节点列表</param>
    /// <returns>返回节点数据字典</returns>
    public Dictionary<string, DataValue> GetBatchNodeDatasOfSync(List<NodeId> nodeIdList)
    {
        Dictionary<string, DataValue> dicNodeInfo = new Dictionary<string, DataValue>();
        if (nodeIdList != null && nodeIdList.Count > 0 && ConnectStatus)
        {
            try
            {
                List<DataValue> dataValues = opcUaClient.ReadNodes(nodeIdList.ToArray());

                int count = nodeIdList.Count;
                for (int i = 0; i < count; i++)
                {
                    AddInfoToDic(dicNodeInfo, nodeIdList[i].ToString(), dataValues[i]);
                }
            }
            catch (Exception ex)
            {
                HandleException("读取失败！！！", ex);
            }
        }

        return dicNodeInfo;
    }


    /// <summary>
    /// 获取到当前节点的值【异步读取】
    /// </summary>
    /// <typeparam name="T">节点对应的数据类型</typeparam>
    /// <param name="nodeId">节点</param>
    /// <returns>返回当前节点的值</returns>
    public async Task<T> GetCurrentNodeValueOfAsync<T>(string nodeId)
    {
        T value = default(T);
        if (!string.IsNullOrEmpty(nodeId) && ConnectStatus)
        {
            try
            {
                value = await opcUaClient.ReadNodeAsync<T>(nodeId);
            }
            catch (Exception ex)
            {
                HandleException("读取失败！！！", ex);
            }
        }

        return value;
    }

    /// <summary>
    /// 获取到批量节点数据【异步读取】
    /// </summary>
    /// <param name="nodeIds">节点列表</param>
    /// <returns>返回节点数据字典</returns>
    public async Task<Dictionary<string, DataValue>> GetBatchNodeDatasOfAsync(List<NodeId> nodeIdList)
    {
        Dictionary<string, DataValue> dicNodeInfo = new Dictionary<string, DataValue>();
        if (nodeIdList != null && nodeIdList.Count > 0 && ConnectStatus)
        {
            try
            {
                List<DataValue> dataValues = await opcUaClient.ReadNodesAsync(nodeIdList.ToArray());

                int count = nodeIdList.Count;
                for (int i = 0; i < count; i++)
                {
                    AddInfoToDic(dicNodeInfo, nodeIdList[i].ToString(), dataValues[i]);
                }
            }
            catch (Exception ex)
            {
                HandleException("读取失败！！！", ex);
            }
        }

        return dicNodeInfo;
    }


    /// <summary>
    /// 获取到当前节点的关联节点
    /// </summary>
    /// <param name="nodeId">当前节点</param>
    /// <returns>返回当前节点的关联节点</returns>
    public ReferenceDescription[] GetAllRelationNodeOfNodeId(string nodeId)
    {
        ReferenceDescription[] referenceDescriptions = null;

        if (!string.IsNullOrEmpty(nodeId) && ConnectStatus)
        {
            try
            {
                referenceDescriptions = opcUaClient.BrowseNodeReference(nodeId);
                //    ReferenceDescription refDesc = // ReferenceDescription object

            }
            catch (Exception ex)
            {
                string str = "获取当前： " + nodeId + "  节点的相关节点失败！！！";
                HandleException(str, ex);
            }
        }

        return referenceDescriptions;
    }


    /// <summary>
    /// 获取到当前节点的关联节点
    /// </summary>
    /// <param name="nodeId">当前节点</param>
    /// <returns>返回当前节点的关联节点</returns>
    public Dictionary<string, DataValue> GetAllRelationNodeValue(string nodeId)
    {
        Dictionary<string, DataValue> dicNodeInfo = new Dictionary<string, DataValue>();
        ReferenceDescription[] referenceDescriptions = null;

        if (!string.IsNullOrEmpty(nodeId) && ConnectStatus)
        {
            try
            {
                referenceDescriptions = opcUaClient.BrowseNodeReference(nodeId);
                var nodeIdList = referenceDescriptions
                    .Select(refDesc => new NodeId(refDesc.NodeId.ToString())).ToList();

                List<DataValue> dataValues = opcUaClient.ReadNodes(nodeIdList.ToArray());

                var count = nodeIdList.Count;
                for (var i = 0; i < count; i++)
                {
                    AddInfoToDic(dicNodeInfo, nodeIdList[i].ToString(), dataValues[i]);
                }

            }
            catch (Exception ex)
            {
                string str = "获取当前： " + nodeId + "  节点的相关节点失败！！！";
                HandleException(str, ex);
            }
        }

        return dicNodeInfo;
    }


    /// <summary>
    /// 获取到当前节点的所有属性
    /// </summary>
    /// <param name="nodeId">当前节点</param>
    /// <returns>返回当前节点对应的所有属性</returns>
    public OpcNodeAttribute[] GetCurrentNodeAttributes(string nodeId)
    {
        OpcNodeAttribute[] opcNodeAttributes = null;

        if (!string.IsNullOrEmpty(nodeId) && ConnectStatus)
        {
            try
            {
                opcNodeAttributes = opcUaClient.ReadNoteAttributes(nodeId);
            }
            catch (Exception ex)
            {
                string str = "读取节点；" + nodeId + "  的所有属性失败！！！";
                HandleException(str, ex);
            }
        }

        return opcNodeAttributes;
    }

    /// <summary>
    /// 写入单个节点【同步方式】
    /// </summary>
    /// <typeparam name="T">写入节点值得数据类型</typeparam>
    /// <param name="nodeId">节点</param>
    /// <param name="value">节点对应的数据值(比如:(short)123）)</param>
    /// <returns>返回写入结果（true:表示写入成功）</returns>
    public bool WriteSingleNodeIdOfSync<T>(string nodeId, T value)
    {
        bool success = false;

        if (ConnectStatus)
        {
            if (!string.IsNullOrEmpty(nodeId))
            {
                try
                {
                    success = opcUaClient.WriteNode(nodeId, value);
                }
                catch (Exception ex)
                {
                    string str = "当前节点：" + nodeId + "  写入失败";
                    HandleException(str, ex);
                }
            }
        }

        return success;
    }

    /// <summary>
    /// 批量写入节点
    /// </summary>
    /// <param name="nodeIdArray">节点数组</param>
    /// <param name="nodeIdValueArray">节点对应数据数组</param>
    /// <returns>返回写入结果（true:表示写入成功）</returns>
    public bool BatchWriteNodeIds(string[] nodeIdArray, object[] nodeIdValueArray)
    {
        bool success = false;
        if (nodeIdArray != null && nodeIdArray.Length > 0 &&
            nodeIdValueArray != null && nodeIdValueArray.Length > 0)

        {
            try
            {
                success = opcUaClient.WriteNodes(nodeIdArray, nodeIdValueArray);
            }
            catch (Exception ex)
            {
                HandleException("批量写入节点失败！！！", ex);
            }
        }

        return success;
    }

    /// <summary>
    /// 写入单个节点【异步方式】
    /// </summary>
    /// <typeparam name="T">写入节点值得数据类型</typeparam>
    /// <param name="nodeId">节点</param>
    /// <param name="value">节点对应的数据值</param>
    /// <returns>返回写入结果（true:表示写入成功）</returns>
    public async Task<bool> WriteSingleNodeIdOfAsync<T>(string nodeId, T value)
    {
        bool success = false;

        if (ConnectStatus)
        {
            if (!string.IsNullOrEmpty(nodeId))
            {
                try
                {
                    success = await opcUaClient.WriteNodeAsync(nodeId, value);
                }
                catch (Exception ex)
                {
                    string str = "当前节点：" + nodeId + "  写入失败";
                    HandleException(str, ex);
                }
            }
        }

        return success;
    }


    /// <summary>
    /// 读取单个节点的历史数据记录
    /// </summary>
    /// <typeparam name="T">节点的数据类型</typeparam>
    /// <param name="nodeId">节点</param>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <returns>返回该节点对应的历史数据记录</returns>
    public List<T> ReadSingleNodeIdHistoryDatas<T>(string nodeId, DateTime startTime, DateTime endTime)
    {
        List<T> nodeIdDatas = null;
        if (!string.IsNullOrEmpty(nodeId) && endTime > startTime)
        {
            try
            {
                nodeIdDatas = opcUaClient.ReadHistoryRawDataValues<T>(nodeId, startTime, endTime).ToList();
            }
            catch (Exception ex)
            {
                HandleException("读取失败", ex);
            }
        }

        return nodeIdDatas;
    }

    /// <summary>
    /// 读取单个节点的历史数据记录
    /// </summary>
    /// <typeparam name="T">节点的数据类型</typeparam>
    /// <param name="nodeId">节点</param>
    /// <param name="startTime">开始时间</param>
    /// <param name="endTime">结束时间</param>
    /// <returns>返回该节点对应的历史数据记录</returns>
    public List<DataValue> ReadSingleNodeIdHistoryDatas(string nodeId, DateTime startTime, DateTime endTime)
    {
        List<DataValue> nodeIdDatas = null;
        if (!string.IsNullOrEmpty(nodeId) && startTime != null && endTime != null && endTime > startTime)
        {
            if (ConnectStatus)
            {
                try
                {
                    nodeIdDatas = opcUaClient.ReadHistoryRawDataValues(nodeId, startTime, endTime).ToList();
                }
                catch (Exception ex)
                {
                    HandleException("读取失败", ex);
                }
            }
        }

        return nodeIdDatas;
    }


    /// <summary>
    /// 单节点数据订阅
    /// </summary>
    /// <param name="key">订阅的关键字（必须唯一）</param>
    /// <param name="nodeId">节点</param>
    /// <param name="callback">数据订阅的回调方法</param>
    public void SingleNodeIdDatasSubscription(string key, string nodeId,
        Action<string, MonitoredItem, MonitoredItemNotificationEventArgs> callback)
    {
        if (ConnectStatus)
        {
            try
            {
                opcUaClient.AddSubscription(key, nodeId, callback);
            }
            catch (Exception ex)
            {
                string str = "订阅节点：" + nodeId + " 数据失败！！！";
                HandleException(str, ex);
            }
        }
    }

    private void HandleException(string name, Exception ex)
    {
        Console.WriteLine(name, ex.Message);
    }
    /// <summary>
    /// 取消单节点数据订阅
    /// </summary>
    /// <param name="key">订阅的关键字</param>
    public bool CancelSingleNodeIdDatasSubscription(string key)
    {
        bool success = false;
        if (!string.IsNullOrEmpty(key))
        {
            if (ConnectStatus)
            {
                try
                {
                    opcUaClient.RemoveSubscription(key);
                    success = true;
                }
                catch (Exception ex)
                {
                    string str = "取消 " + key + " 的订阅失败";
                    HandleException(str, ex);
                }
            }
        }

        return success;
    }


    /// <summary>
    /// 批量节点数据订阅
    /// </summary>
    /// <param name="key">订阅的关键字（必须唯一）</param>
    /// <param name="nodeIds">节点数组</param>
    /// <param name="callback">数据订阅的回调方法</param>
    public void BatchNodeIdDatasSubscription(string key, string[] nodeIds,
        Action<string, MonitoredItem, MonitoredItemNotificationEventArgs> callback)
    {
        if (!string.IsNullOrEmpty(key) && nodeIds != null && nodeIds.Length > 0)
        {
            if (ConnectStatus)
            {
                try
                {
                    opcUaClient.AddSubscription(key, nodeIds, callback);
                }
                catch (Exception ex)
                {
                    string str = "批量订阅节点数据失败！！！";
                    HandleException(str, ex);
                }
            }
        }
    }

    /// <summary>
    /// 取消所有节点的数据订阅
    /// </summary>
    /// <returns></returns>
    public bool CancelAllNodeIdDatasSubscription()
    {
        bool success = false;

        if (ConnectStatus)
        {
            try
            {
                opcUaClient.RemoveAllSubscription();
                success = true;
            }
            catch (Exception ex)
            {
                HandleException("取消所有的节点数据订阅失败！！！", ex);
            }
        }

        return success;
    }

    #endregion


    #region 私有方法

    /// <summary>
    /// 添加数据到字典中（相同键的则采用最后一个键对应的值）
    /// </summary>
    /// <param name="dic">字典</param>
    /// <param name="key">键</param>
    /// <param name="dataValue">值</param>
    private void AddInfoToDic(Dictionary<string, DataValue> dic, string key, DataValue dataValue)
    {
        if (dic != null)
        {
            if (!dic.ContainsKey(key))
            {
                dic.Add(key, dataValue);
            }
            else
            {
                dic[key] = dataValue;
            }
        }
    }

    #endregion
}
//Class_end