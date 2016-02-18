using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Abp.Domain.Services;

namespace SimpleCms.VimeApp.VimeAppHelpers
{
    public class FileHelper : DomainService, IFileHelper
    {
        private readonly HttpServerUtility _server;

        public FileHelper()
        {
            _server = HttpContext.Current.Server;
        }

        public string BaseSaveRoute => "/Content/Public/Images/";
        public string SaveFileNGenerateRoute(HttpPostedFileBase file, string uniqueFolderName)
        {
            var sqlFolder = BaseSaveRoute + uniqueFolderName + "/" + file.FileName;
            file.SaveAs(_server.MapPath(sqlFolder));
            return sqlFolder;
        }
    }
}
