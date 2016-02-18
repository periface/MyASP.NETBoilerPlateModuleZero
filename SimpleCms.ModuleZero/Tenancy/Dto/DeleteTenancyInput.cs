using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Runtime.Session;

namespace SimpleCms.ModuleZero.Tenancy.Dto
{
    public class DeleteTenancyInput
    {
        /// <summary>
        /// Protected to avoid diferent tenancy inputs
        /// </summary>
        public int IdTenancy { get; protected set; }
        protected DeleteTenancyInput()
        {

        }

        public DeleteTenancyInput(IAbpSession abpSession)
        {
            CreateDeleteModel(abpSession);
        }
        public DeleteTenancyInput CreateDeleteModel(IAbpSession abpSession)
        {
            if (abpSession.TenantId == null) return null;
            var model = new DeleteTenancyInput()
            {
                IdTenancy = abpSession.TenantId.Value
            };
            return model;
        }
    }
}
