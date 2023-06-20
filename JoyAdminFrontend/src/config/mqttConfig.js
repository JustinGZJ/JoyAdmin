export default {
  host: 'broker.emqx.io',
  port: 8083,
  endpoint: '/mqtt',
  clean: true, // 保留会话
  connectTimeout: 4000, // 超时时间
  reconnectPeriod: 10000, // 重连时间间隔
  // 认证信息
  clientId: 'mqttjs_' + Math.random().toString(16).slice(2), // 客户端ID
  username: 'emqx_test', // 用户名
  password: 'emqx_test' // 密码
}
