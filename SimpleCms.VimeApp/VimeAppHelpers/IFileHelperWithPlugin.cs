using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SimpleCms.VimeApp.VimeAppHelpers
{
    public interface IFileHelperWithPlugin : IFileHelper
    {
        string SaveImageFile(HttpPostedFileBase imageFile, string uniqueFolderName);
        string SaveImageFile(HttpPostedFileBase imageFile, string uniqueFolderName, int width, int height);
    }
}
