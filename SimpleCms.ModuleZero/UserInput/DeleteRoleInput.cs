using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCms.ModuleZero.UserInput
{
    public class DeleteRoleInput
    {
        public int RoleId { get; set; }
        public int? TenantId { get; set; }
    }
}
