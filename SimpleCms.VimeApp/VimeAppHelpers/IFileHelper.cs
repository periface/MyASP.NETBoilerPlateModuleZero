using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Abp.Domain.Services;

namespace SimpleCms.VimeApp.VimeAppHelpers
{
    public interface IFileHelper : IDomainService
    {
        string BaseSaveRoute { get; }
        string SaveFileNGenerateRoute(HttpPostedFileBase file, string uniqueFolderName);
    }
}
