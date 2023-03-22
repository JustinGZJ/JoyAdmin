using Furion;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoyAdmin.Core
{
     public class HttpHelper
    {
        /// <summary>
        /// 获取用户网络信息
        /// </summary>
        /// <returns></returns>
        public static NetInfo getNetInfo()
        {

            var httpContextAccessor = App.GetService<IHttpContextAccessor>();
            if (httpContextAccessor == null || httpContextAccessor.HttpContext == null) return default;

            var ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            //nginx 反向代理后真实ip
            var realIp = httpContextAccessor.HttpContext.Request.Headers["X-Real-IP"].FirstOrDefault();
            var referer = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Referer].ToString();
            var userAgent = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.UserAgent].ToString();
            return new NetInfo
            {
                Ip = ip,
                RealIp = realIp,
                UserAgent = userAgent,
                Referer = referer
            };

        }
             
            
        
    }
}
