using System.Web;
using Abp.Application.Services;

namespace SimpleCms.ImageManagement.ImageService
{
    public interface IImageService : IApplicationService
    {
        string SaveImageImgRezisingNet(HttpPostedFileBase image,string virtualPath);
        string SaveImageImgRezisingNet(HttpPostedFileBase image,string virtualPath, int width,int height);
    }
}
