using System;
using System.Net;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;
using GoogleRecaptchaDotNetMvc.Models;
using Newtonsoft.Json;

namespace GoogleRecaptchaDotNetMvc.Attribute
{
    public class CheckRecaptcha : ActionFilterAttribute, IActionFilter
    {
        private readonly string[] _routeValues = { };
        public CheckRecaptcha(params string[] routeValues)
        {
            _routeValues = routeValues;
        }

        public CheckRecaptcha()
        {

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.RequestContext.HttpContext.Request["g-recaptcha-response"];
            var secret = WebConfigurationManager.AppSettings["RecaptchaGoogleKey"];
            var client = new WebClient();
            var petition =
                client.DownloadString(
                    $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={response}");
            var captchaReponse = JsonConvert.DeserializeObject<ResultModel>(petition);
            if (captchaReponse.Success)
            {
                //Excecute function normally
                OnActionExecuting(filterContext);
            }
            else
            {
                ExcecuteActionOnErrorCode(captchaReponse, filterContext);
            }
        }
        
        private void ExcecuteActionOnErrorCode(ResultModel captchaReponse, ActionExecutingContext filterContext)
        {
            
        }
        private RedirectToRouteResult CreateResult(string controller, string action, ActionExecutingContext filterContext)
        {
            var routeValueDictionary = new RouteValueDictionary()
            {
                {"controller", controller},
                {"action", action},
            };
            foreach (var routeValue in _routeValues)
            {
                try
                {
                    var value = filterContext.RouteData.Values[routeValue];
                    routeValueDictionary.Add(routeValue, value);
                }
                catch (Exception)
                {
                    throw new InvalidOperationException("Value not found in url " + routeValue);
                }
            }
            return new RedirectToRouteResult(routeValueDictionary);
        }
    }
}
