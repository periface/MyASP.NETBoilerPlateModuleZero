using System.Web;

namespace SimpleCms.ModuleCms.Inputs
{
    public class SiteInfoImageInput
    {
        public string Discriminator { get; set; }
        public int IdConfig { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}
