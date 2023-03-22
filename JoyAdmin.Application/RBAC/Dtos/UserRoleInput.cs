using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoyAdmin.Application 
{
    public class UserRoleInput
    {
        
        public long UserId { get; set; }

        public long[] RoleIds { get; set; }
    }
}
