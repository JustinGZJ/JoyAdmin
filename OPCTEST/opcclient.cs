using Opc.Ua;
using Opc.Ua.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace hg.opc.client
{
    public class OpcuaClient
    {
        #region Private Fields
        private readonly ApplicationConfiguration _appConfig;
        private ISession _clientSession;
        private bool _isConnected = false;                       //是否已经连接过
        private readonly int _reconnectPeriod = 10;               // 重连状态
        private SessionReconnectHandler _sessionReconnectHandler;
        private EventHandler _onConnectedEventHandler;
        private Dictionary<string, Subscription> _subscriptionNodes=new Dictionary<string, Subscription>();        // 系统所有的节点信息
        #endregion Private Fields
 
        #region Public Members
 
        public IUserIdentity UserIdentity { get; set; }= new UserIdentity(new AnonymousIdentityToken());
        public bool IsConnected { get => _isConnected; private set => _isConnected = value; }
        public Dictionary<string, Subscription> SubscriptionNodes { get => _subscriptionNodes; set => _subscriptionNodes = value; }
 
        public event EventHandler OnConnectedEvent
        {
            add { _onConnectedEventHandler += value; }
            remove { _onConnectedEventHandler -= value; }
        }
        #endregion
 
        #region 构造函数
        public OpcuaClient()
        {
            var certificateValidator = new CertificateValidator();
            certificateValidator.CertificateValidation += (sender, eventArgs) =>
            {
                if (ServiceResult.IsGood(eventArgs.Error))
                    eventArgs.Accept = true;
                else if (eventArgs.Error.StatusCode.Code == StatusCodes.BadCertificateUntrusted)
                    eventArgs.Accept = true;
                else
                    throw new Exception(string.Format("证书验证错误： {0}: {1}", eventArgs.Error.Code, eventArgs.Error.AdditionalInfo));
            };
 
            certificateValidator.Update(new SecurityConfiguration
            {
                AutoAcceptUntrustedCertificates = true,
                RejectSHA1SignedCertificates = false,
                MinimumCertificateKeySize = 1024,
            });
 
            var configuration = new ApplicationConfiguration
            {
                ApplicationName = "MyOpc_Client",
                ApplicationType = ApplicationType.Client,
                CertificateValidator = certificateValidator,
                ApplicationUri = string.Empty, 
                ServerConfiguration = new ServerConfiguration
                {
                    MaxSubscriptionCount = 100000,
                    MaxMessageQueueSize = 100000,
                    MaxNotificationQueueSize = 100000,
                    MaxPublishRequestCount = 100000,
                },
                SecurityConfiguration = new SecurityConfiguration
                {
                    AutoAcceptUntrustedCertificates = true,
                    RejectSHA1SignedCertificates = false,
                    MinimumCertificateKeySize = 1024,
                    SuppressNonceValidationErrors = true,
 
                    ApplicationCertificate = new CertificateIdentifier
                    {
                        StoreType = CertificateStoreType.X509Store,
                        StorePath = "CurrentUser\\My",
                    },
                    TrustedIssuerCertificates = new CertificateTrustList
                    {
                        StoreType = CertificateStoreType.X509Store,
                        StorePath = "CurrentUser\\Root",
                    },
                    TrustedPeerCertificates = new CertificateTrustList
                    {
                        StoreType = CertificateStoreType.X509Store,
                        StorePath = "CurrentUser\\Root",
                    }
                },
                TransportQuotas = new TransportQuotas
                {
                    OperationTimeout = 6000000,
                    MaxStringLength = int.MaxValue,
                    MaxByteStringLength = int.MaxValue,
                    MaxArrayLength = 65535,
                    MaxMessageSize = 419430400,
                    MaxBufferSize = 65535,
                    ChannelLifetime = -1,
                    SecurityTokenLifetime = -1
                },
                ClientConfiguration = new ClientConfiguration
                {
                    DefaultSessionTimeout = -1,
                    MinSubscriptionLifetime = -1,
                },
                DisableHiResClock = true
            };
 
            configuration.Validate(ApplicationType.Client);
            _appConfig = configuration;
        }
        #endregion
        #region 连接
        public async Task ConnectServer(string serverUrl)
        {
            Disconnect();
            var endpointDescription = CoreClientUtils.SelectEndpoint(serverUrl, false);
            var endpointConfiguration = EndpointConfiguration.Create(_appConfig);
            var endpoint = new ConfiguredEndpoint(null, endpointDescription, endpointConfiguration);
            _clientSession = await Task.Run(async ()=> {
                
                return await Session.Create(
                    _appConfig,
                    endpoint,
                    false,
                    false,
                    _appConfig.ApplicationName,
                    60000,
                    UserIdentity,
                    new string[] { });
            });
            _clientSession.KeepAlive += Session_KeepAlive;
            IsConnected = true;
            _onConnectedEventHandler?.Invoke(this, EventArgs.Empty);
        }
 
        private void Session_KeepAlive(ISession session, KeepAliveEventArgs e)
        {
            try
            {
                if (!object.ReferenceEquals(session, this._clientSession))
                {
                    return;
                }
                if (ServiceResult.IsBad(e.Status))
                {
                    if (_reconnectPeriod <= 0)
                    {
                        return;
                    }

                    if (_sessionReconnectHandler != null) return;
                    _sessionReconnectHandler = new SessionReconnectHandler();
                    _sessionReconnectHandler.BeginReconnect(session, _reconnectPeriod * 1000, Server_ReconnectComplete);
                    return;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
 
        private void Server_ReconnectComplete(object sender, EventArgs e)
        {
            try
            {
                if (!ReferenceEquals(sender, _sessionReconnectHandler))
                {
                    return;
                }
                _clientSession = _sessionReconnectHandler.Session;
                _sessionReconnectHandler.Dispose();
                _sessionReconnectHandler = null;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
 
        public void Disconnect()
        {
            if (_sessionReconnectHandler != null)
            {
                _sessionReconnectHandler.Dispose();
                _sessionReconnectHandler = null;
            }
 
            if (_clientSession != null)
            {
                _clientSession.Close(10000);
                _clientSession = null;
            }
            IsConnected = false;
        }
        #endregion
 
        #region 浏览节点
 
        public ReferenceDescriptionCollection BrowserNode2(NodeId nodeId)
        {
            var browser = new Browser(_clientSession);
            return browser.Browse(nodeId);
        }
 
        public ReferenceDescriptionCollection BrowserNode(NodeId nodeId)
        { 
            var browseDescriptions = new BrowseDescriptionCollection();
            var browserDesc = new BrowseDescription();
            browserDesc.NodeId = nodeId;
            browserDesc.BrowseDirection = BrowseDirection.Forward;
            browserDesc.ReferenceTypeId = ReferenceTypeIds.Aggregates;
            browserDesc.IncludeSubtypes = true;
            browserDesc.NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.View | NodeClass.VariableType | NodeClass.DataType);
            browserDesc.ResultMask = (uint)BrowseResultMask.All;
 
            var browseDesc2 = new BrowseDescription();
            browseDesc2.NodeId = nodeId;
            browseDesc2.BrowseDirection = BrowseDirection.Forward;
            browseDesc2.ReferenceTypeId = ReferenceTypeIds.Organizes;
            browseDesc2.IncludeSubtypes = true;
            browseDesc2.NodeClassMask = (uint)(NodeClass.Object | NodeClass.Variable | NodeClass.Method | NodeClass.View | NodeClass.ReferenceType | NodeClass.ObjectType | NodeClass.VariableType | NodeClass.DataType);
            browseDesc2.ResultMask = (uint)BrowseResultMask.All;
           
 
            browseDescriptions.Add(browserDesc);
            browseDescriptions.Add(browseDesc2);
 
            ReferenceDescriptionCollection references = GetReferenceDescriptionCollection(browseDescriptions);
            return references;
        }
 
        public ReferenceDescriptionCollection GetReferenceDescriptionCollection(BrowseDescriptionCollection browseDescriptions)
        {
            try
            {
                var referenceDescriptions = new ReferenceDescriptionCollection();
                var unprocessedOperations = new BrowseDescriptionCollection();
 
                while (browseDescriptions.Count > 0)
                {
                    BrowseResultCollection results = null;
                    DiagnosticInfoCollection diagnosticInfos = null;
                    _clientSession.Browse(null, null,  0, browseDescriptions,out results,out diagnosticInfos);
 
                    ClientBase.ValidateResponse(results, browseDescriptions);
                    ClientBase.ValidateDiagnosticInfos(diagnosticInfos, browseDescriptions);
 
                    ByteStringCollection continuationPoints = new ByteStringCollection();
                    for (int i = 0; i < browseDescriptions.Count; i++)
                    {
                        if (StatusCode.IsBad(results[i].StatusCode))
                        {
                            if (results[i].StatusCode == StatusCodes.BadNoContinuationPoints)
                            {
                                unprocessedOperations.Add(browseDescriptions[i]);
                            }
                            continue;
                        }
 
                        // check if all references have been fetched.
                        if (results[i].References.Count == 0)
                        {
                            continue;
                        }
                        referenceDescriptions.AddRange(results[i].References);
                        if (results[i].ContinuationPoint != null)
                        {
                            continuationPoints.Add(results[i].ContinuationPoint);
                        }
                    }
                    ByteStringCollection revisedContiuationPoints = new ByteStringCollection();
                    while (continuationPoints.Count > 0)
                    {
                        // continue browse operation.
                        _clientSession.BrowseNext(
                            null,
                            true,
                            continuationPoints,
                            out results,
                            out diagnosticInfos);
 
                        ClientBase.ValidateResponse(results, continuationPoints);
                        ClientBase.ValidateDiagnosticInfos(diagnosticInfos, continuationPoints);
 
                        for (int j = 0; j < continuationPoints.Count; j++)
                        {
                            if (StatusCode.IsBad(results[j].StatusCode))
                            {
                                continue;
                            }
                            if (results[j].References.Count == 0)
                            {
                                continue;
                            }
 
                            referenceDescriptions.AddRange(results[j].References);
 
                            if (results[j].ContinuationPoint != null)
                            {
                                revisedContiuationPoints.Add(results[j].ContinuationPoint);
                            }
                        }
                        revisedContiuationPoints = continuationPoints;
                    }
                    browseDescriptions = unprocessedOperations;
                }
                return referenceDescriptions;
            }
            catch (Exception exception)
            {
                return null;
            }
        }
 
 
        #endregion
 
        #region Node Write/Read Support
 
        /// <summary>
        /// Read a value node from server
        /// </summary>
        /// <param name="nodeId">node id</param>
        /// <returns>DataValue</returns>
        public DataValue ReadNode(NodeId nodeId)
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection
            {
                new( )
                {
                    NodeId = nodeId,
                    AttributeId = Attributes.Value
                }
            };
 
            // read the current value
            _clientSession.Read(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                out DataValueCollection results,
                out DiagnosticInfoCollection diagnosticInfos);
 
            ClientBase.ValidateResponse(results, nodesToRead);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);
 
            return results[0];
        }
 
        /// <summary>
        /// 是否可读写节点
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public bool IsWriteableNode(NodeId nodeId)
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection
            {
                new( )
                {
                    NodeId = nodeId,
                    AttributeId = Attributes.AccessLevel
                }
            };
 
            // read the current value
            _clientSession.Read(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                out DataValueCollection results,
                out DiagnosticInfoCollection diagnosticInfos);
 
            ClientBase.ValidateResponse(results, nodesToRead);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);
 
            DataValue value= results[0];
            if (value.WrappedValue == Variant.Null)
            {
                return true;
            }
            return !((byte)value.WrappedValue.Value == 1);
 
        }
 
        /// <summary>
        /// Read a value node from server
        /// </summary>
        /// <typeparam name="T">type of value</typeparam>
        /// <param name="tag">node id</param>
        /// <returns>实际值</returns>
        public T ReadNode<T>(string tag)
        {
            DataValue dataValue = ReadNode(new NodeId(tag));
            return (T)dataValue.Value;
        }
 
        /// <summary>
        /// Read a tag asynchronously
        /// </summary>
        /// <typeparam name="T">The type of tag to read</typeparam>
        /// <param name="tag">tag值</param>
        /// <returns>The value retrieved from the OPC</returns>
        public Task<T> ReadNodeAsync<T>(string tag)
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection
            {
                new ReadValueId()
                {
                    NodeId = new NodeId(tag),
                    AttributeId = Attributes.Value
                }
            };
 
            // Wrap the ReadAsync logic in a TaskCompletionSource, so we can use C# async/await syntax to call it:
            var taskCompletionSource = new TaskCompletionSource<T>();
            _clientSession.BeginRead(
                requestHeader: null,
                maxAge: 0,
                timestampsToReturn: TimestampsToReturn.Neither,
                nodesToRead: nodesToRead,
                callback: ar =>
                {
                    DataValueCollection results;
                    DiagnosticInfoCollection diag;
                    var response = _clientSession.EndRead(
                      result: ar,
                      results: out results,
                      diagnosticInfos: out diag);
 
                    try
                    {
                        if (!StatusCode.IsGood(response.ServiceResult))
                            throw new Exception(string.Format("Invalid response from the server. (Response Status: {0})", response.ServiceResult));
 
                        if (!StatusCode.IsGood(results[0].StatusCode))
                            throw new Exception(string.Format("Invalid response from the server. (Response Status: {0})", results[0].StatusCode));
                        var val = results[0];
                        taskCompletionSource.TrySetResult((T)val.Value);
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.TrySetException(ex);
                    }
                },
                asyncState: null);
 
            return taskCompletionSource.Task;
        }
 
        /// <summary>
        /// read several value nodes from server
        /// </summary>
        /// <param name="nodeIds">all NodeIds</param>
        /// <returns>all values</returns>
        public List<DataValue> ReadNodes(NodeId[] nodeIds)
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            for (int i = 0; i < nodeIds.Length; i++)
            {
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = nodeIds[i],
                    AttributeId = Attributes.Value
                });
            }
 
            // 读取当前的值
            _clientSession.Read(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                out DataValueCollection results,
                out DiagnosticInfoCollection diagnosticInfos);
 
            ClientBase.ValidateResponse(results, nodesToRead);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);
 
            return results.ToList();
        }
 
        /// <summary>
        /// read several value nodes from server
        /// </summary>
        /// <param name="nodeIds">all NodeIds</param>
        /// <returns>all values</returns>
        public Task<List<DataValue>> ReadNodesAsync(NodeId[] nodeIds)
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            for (int i = 0; i < nodeIds.Length; i++)
            {
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = nodeIds[i],
                    AttributeId = Attributes.Value
                });
            }
 
            var taskCompletionSource = new TaskCompletionSource<List<DataValue>>();
            // 读取当前的值
            _clientSession.BeginRead(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                callback: ar =>
                {
                    DataValueCollection results;
                    DiagnosticInfoCollection diag;
                    var response = _clientSession.EndRead(
                      result: ar,
                      results: out results,
                      diagnosticInfos: out diag);
 
                    try
                    {
                        CheckReturnValue(response.ServiceResult);
                        taskCompletionSource.TrySetResult(results.ToList());
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.TrySetException(ex);
                    }
                },
                asyncState: null);
 
            return taskCompletionSource.Task;
        }
 
        /// <summary>
        /// read several value nodes from server
        /// </summary>
        /// <param name="tags">所以的节点数组信息</param>
        /// <returns>all values</returns>
        public List<T> ReadNodes<T>(string[] tags)
        {
            List<T> result = new List<T>();
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            for (int i = 0; i < tags.Length; i++)
            {
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = new NodeId(tags[i]),
                    AttributeId = Attributes.Value
                });
            }
 
            // 读取当前的值
            _clientSession.Read(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                out DataValueCollection results,
                out DiagnosticInfoCollection diagnosticInfos);
 
            ClientBase.ValidateResponse(results, nodesToRead);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);
 
            foreach (var item in results)
            {
                result.Add((T)item.Value);
            }
            return result;
        }
 
        /// <summary>
        /// read several value nodes from server
        /// </summary>
        /// <param name="tags">all NodeIds</param>
        /// <returns>all values</returns>
        public Task<List<T>> ReadNodesAsync<T>(string[] tags)
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            for (int i = 0; i < tags.Length; i++)
            {
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = new NodeId(tags[i]),
                    AttributeId = Attributes.Value
                });
            }
 
            var taskCompletionSource = new TaskCompletionSource<List<T>>();
            // 读取当前的值
            _clientSession.BeginRead(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                callback: ar =>
                {
                    DataValueCollection results;
                    DiagnosticInfoCollection diag;
                    var response = _clientSession.EndRead(
                      result: ar,
                      results: out results,
                      diagnosticInfos: out diag);
 
                    try
                    {
                        CheckReturnValue(response.ServiceResult);
                        List<T> result = new List<T>();
                        foreach (var item in results)
                        {
                            result.Add((T)item.Value);
                        }
                        taskCompletionSource.TrySetResult(result);
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.TrySetException(ex);
                    }
                },
                asyncState: null);
 
            return taskCompletionSource.Task;
        }
 
 
 
        /// <summary>
        /// 0:NodeClass  1:Value  2:AccessLevel  3:DisplayName  4:Description
        /// </summary>
        /// <param name="nodeIds"></param>
        /// <returns></returns>
        public DataValue[] ReadNodeAttributes(List<NodeId> nodeIds)
        {
            ReadValueIdCollection nodesToRead = new ReadValueIdCollection();
            foreach (var nodeId in nodeIds)
            {
                NodeId sourceId = nodeId;
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.NodeClass
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.Value
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.AccessLevel
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.DisplayName
                });
                nodesToRead.Add(new ReadValueId()
                {
                    NodeId = sourceId,
                    AttributeId = Attributes.Description
                });
            }
 
            // read all values.
            _clientSession.Read(
                null,
                0,
                TimestampsToReturn.Neither,
                nodesToRead,
                out DataValueCollection results,
                out DiagnosticInfoCollection diagnosticInfos);
 
            ClientBase.ValidateResponse(results, nodesToRead);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, nodesToRead);
 
            return results.ToArray();
        }
 
 
        /// <summary>
        /// write a note to server(you should use try catch)
        /// </summary>
        /// <typeparam name="T">The type of tag to write on</typeparam>
        /// <param name="tag">节点名称</param>
        /// <param name="value">值</param>
        /// <returns>if success True,otherwise False</returns>
        public bool WriteNode<T>(string tag, T value)
        {
            WriteValue valueToWrite = new WriteValue()
            {
                NodeId = new NodeId(tag),
                AttributeId = Attributes.Value
            };
            valueToWrite.Value.Value = value;
            valueToWrite.Value.StatusCode = StatusCodes.Good;
            valueToWrite.Value.ServerTimestamp = DateTime.MinValue;
            valueToWrite.Value.SourceTimestamp = DateTime.MinValue;
 
            WriteValueCollection valuesToWrite = new WriteValueCollection
            {
                valueToWrite
            };
 
            // 写入当前的值
 
            _clientSession.Write(
                null,
                valuesToWrite,
                out StatusCodeCollection results,
                out DiagnosticInfoCollection diagnosticInfos);
 
            ClientBase.ValidateResponse(results, valuesToWrite);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, valuesToWrite);
 
            if (StatusCode.IsBad(results[0]))
            {
                throw new ServiceResultException(results[0]);
            }
 
            return !StatusCode.IsBad(results[0]);
        }
 
        /// <summary>
        /// Write a value on the specified opc tag asynchronously
        /// </summary>
        /// <typeparam name="T">The type of tag to write on</typeparam>
        /// <param name="tag">The fully-qualified identifier of the tag. You can specify a subfolder by using a comma delimited name. E.g: the tag `foo.bar` writes on the tag `bar` on the folder `foo`</param>
        /// <param name="value">The value for the item to write</param>
        public Task<bool> WriteNodeAsync<T>(string tag, T value)
        {
            WriteValue valueToWrite = new WriteValue()
            {
                NodeId = new NodeId(tag),
                AttributeId = Attributes.Value,
            };
            valueToWrite.Value.Value = value;
            valueToWrite.Value.StatusCode = StatusCodes.Good;
            valueToWrite.Value.ServerTimestamp = DateTime.MinValue;
            valueToWrite.Value.SourceTimestamp = DateTime.MinValue;
            WriteValueCollection valuesToWrite = new WriteValueCollection
            {
                valueToWrite
            };
 
            // Wrap the WriteAsync logic in a TaskCompletionSource, so we can use C# async/await syntax to call it:
            var taskCompletionSource = new TaskCompletionSource<bool>();
            _clientSession.BeginWrite(
                requestHeader: null,
                nodesToWrite: valuesToWrite,
                callback: ar =>
                {
               
                    var response = _clientSession.EndWrite(
                      result: ar,
                      results: out StatusCodeCollection results,
                      diagnosticInfos: out DiagnosticInfoCollection diag);
 
                    try
                    {
                        ClientBase.ValidateResponse(results, valuesToWrite);
                        ClientBase.ValidateDiagnosticInfos(diag, valuesToWrite);
                        taskCompletionSource.SetResult(StatusCode.IsGood(results[0]));
                    }
                    catch (Exception ex)
                    {
                        taskCompletionSource.TrySetException(ex);
                    }
                },
                asyncState: null);
            return taskCompletionSource.Task;
        }
 
        /// <summary>
        /// 所有的节点都写入成功，返回<c>True</c>，否则返回<c>False</c>
        /// </summary>
        /// <param name="tags">节点名称数组</param>
        /// <param name="values">节点的值数据</param>
        /// <returns>所有的是否都写入成功</returns>
        public bool WriteNodes(string[] tags, object[] values)
        {
            WriteValueCollection valuesToWrite = new WriteValueCollection();
 
            for (int i = 0; i < tags.Length; i++)
            {
                if (i < values.Length)
                {
                    WriteValue valueToWrite = new WriteValue()
                    {
                        NodeId = new NodeId(tags[i]),
                        AttributeId = Attributes.Value
                    };
                    valueToWrite.Value.Value = values[i];
                    valueToWrite.Value.StatusCode = StatusCodes.Good;
                    valueToWrite.Value.ServerTimestamp = DateTime.MinValue;
                    valueToWrite.Value.SourceTimestamp = DateTime.MinValue;
                    valuesToWrite.Add(valueToWrite);
                }
            }
 
            // 写入当前的值
 
            _clientSession.Write(
                null,
                valuesToWrite,
                out StatusCodeCollection results,
                out DiagnosticInfoCollection diagnosticInfos);
 
            ClientBase.ValidateResponse(results, valuesToWrite);
            ClientBase.ValidateDiagnosticInfos(diagnosticInfos, valuesToWrite);
 
            bool result = true;
            foreach (var r in results)
            {
                if (StatusCode.IsBad(r))
                {
                    result = false;
                    break;
                }
            }
 
            return result;
        }
 
        private void CheckReturnValue(StatusCode status)
        {
            if (!StatusCode.IsGood(status))
                throw new Exception(string.Format("Invalid response from the server. (Response Status: {0})", status));
        }
        #endregion Node Write/Read Support
 
        #region 订阅
 
        /// <summary>
        /// 新增一批订阅，需要指定订阅的关键字，订阅的tag名数组，以及回调方法
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="tags">节点名称数组</param>
        /// <param name="callback">回调方法</param>
        public void AddSubscription(string key, string[] tags, Action<string, MonitoredItem, MonitoredItemNotificationEventArgs> callback)
        {
            Subscription mSubscription = new Subscription(_clientSession.DefaultSubscription);
 
            mSubscription.PublishingEnabled = true;
            mSubscription.PublishingInterval = 0;
            mSubscription.KeepAliveCount = uint.MaxValue;
            mSubscription.LifetimeCount = uint.MaxValue;
            mSubscription.MaxNotificationsPerPublish = uint.MaxValue;
            mSubscription.Priority = 100;
            mSubscription.DisplayName = key;
 
            for (int i = 0; i < tags.Length; i++)
            {
                var item = new MonitoredItem
                {
                    StartNodeId = new NodeId(tags[i]),
                    AttributeId = Attributes.Value,
                    DisplayName = tags[i],
                    SamplingInterval = 100,
                };
                item.Notification += (MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs args) =>
                {
                    callback?.Invoke(key, monitoredItem, args);
                };
                mSubscription.AddItem(item);
            }
 
            _clientSession.AddSubscription(mSubscription);
            mSubscription.Create();
 
            lock (SubscriptionNodes)
            {
                if (SubscriptionNodes.ContainsKey(key))
                {
                    // remove
                    SubscriptionNodes[key].Delete(true);
                    _clientSession.RemoveSubscription(SubscriptionNodes[key]);
                    SubscriptionNodes[key].Dispose();
                    SubscriptionNodes[key] = mSubscription;
                }
                else
                {
                    SubscriptionNodes.Add(key, mSubscription);
                }
            }
        }
 
        /// <summary>
        /// 新增订阅，需要指定订阅的关键字，订阅的tag名数组，以及回调方法
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="tags">节点名称数组</param>
        /// <param name="callback">回调方法</param>
        public void AddSubscription(string key, string tag, Action<string, MonitoredItem, MonitoredItemNotificationEventArgs> callback)
        {
            AddSubscription(key, new string[] { tag }, callback);
        }
 
        /// <summary>
        /// 移除订阅消息，如果该订阅消息是批量的，也直接移除
        /// </summary>
        /// <param name="key">订阅关键值</param>
        public void RemoveSubscription(string key)
        {
            lock (SubscriptionNodes)
            {
                if (SubscriptionNodes.ContainsKey(key))
                {
                    // remove
                    SubscriptionNodes[key].Delete(true);
                    _clientSession.RemoveSubscription(SubscriptionNodes[key]);
                    SubscriptionNodes[key].Dispose();
                    SubscriptionNodes.Remove(key);
                }
            }
        }
 
        /// <summary>
        /// 移除所有的订阅消息
        /// </summary>
        public void RemoveAllSubscription()
        {
            lock (SubscriptionNodes)
            {
                foreach (var item in SubscriptionNodes)
                {
                    item.Value.Delete(true);
                    _clientSession.RemoveSubscription(item.Value);
                    item.Value.Dispose();
                }
                SubscriptionNodes.Clear();
            }
        }
        #endregion
    }
}