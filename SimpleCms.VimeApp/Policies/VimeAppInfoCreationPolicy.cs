using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using SimpleCms.VimeApp.VimeAppEntities;

namespace SimpleCms.VimeApp.Policies
{
    public class VimeAppInfoCreationPolicy : DomainService, IVimeAppInfoCreationPolicy
    {
        private readonly IRepository<VimeAppInfo, Guid> _vimeAppInfoRepository;

        public VimeAppInfoCreationPolicy(IRepository<VimeAppInfo, Guid> vimeAppInfoRepository)
        {
            _vimeAppInfoRepository = vimeAppInfoRepository;
        }

        public void CheckCreationAttemptAsync(VimeAppInfo info)
        {
            if (info.IsActive)
            {
                throw new InvalidOperationException("No puede habilitarse la configuración durante la creación de la misma.");

            }
            if (info.SiteName == null) { throw new ArgumentException("info"); }
        }

        public void CheckExistingName(VimeAppInfo info)
        {
            var vimeAppCheck =
                _vimeAppInfoRepository.FirstOrDefault(a => a.SiteName.ToUpper().Equals(info.SiteName.ToUpper()));
            if (vimeAppCheck != null)
            {
                throw new InvalidOperationException("Ya existe un nombre de sitio con el mismo nombre.");
            }
        }
    }
}
