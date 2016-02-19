using System.Web.Mvc;

namespace SimpleCms.Web.Areas.AdminEcommerce
{
    public class AdminEcommerceAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AdminEcommerce";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AdminEcommerce_default",
                "AdminEcommerce/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}