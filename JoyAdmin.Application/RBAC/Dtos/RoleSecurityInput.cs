using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoyAdmin.Application 
{
    public class RoleSecurityInput
    {
        
        public long RoleId { get; set; }

        public long[] SecurityIds { get; set; }
    }
}
