using Furion.DynamicApiController;
using Furion.FriendlyException;
using Microsoft.AspNetCore.Authorization;
using Serilog;
using Furion.JsonSerialization;

namespace JoyAdmin.Application.System
{


    public class SystemService : IDynamicApiController
    {
        private readonly ILogger _logger; 
        public SystemService(ILogger logger)
        {
            _logger = logger;
        }
    
       
        /// <summary>
        /// 测试错误
        /// </summary>
        [AllowAnonymous]
        public dynamic GetTestError()
        {
            throw Oops.Oh("出错了，OOPS");
        }
        /// <summary>
        /// 保存前端错误
        /// </summary>
        [AllowAnonymous]
        public bool SaveErrorLogger(dynamic info)
        { 
            _logger.Error(JSON.Serialize(info));
            return true;
        }
        /// <summary>
        /// 测试void
        /// </summary>
        [AllowAnonymous]
        public void testvoid()
        {
           
        }
    }
}
