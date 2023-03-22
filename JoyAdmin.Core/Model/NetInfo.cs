using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoyAdmin.Core
{
    public class NetInfo
    {
        /// <summary>
        /// ip地址
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 真实Ip地址
        /// </summary>
        public string RealIp { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        /// UserAgent
        /// </summary>
        public string Referer { get; set; }
    }
}
