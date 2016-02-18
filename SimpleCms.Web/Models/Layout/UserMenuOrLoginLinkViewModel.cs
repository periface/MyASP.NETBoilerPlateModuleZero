using SimpleCms.Sessions.Dto;

namespace SimpleCms.Web.Models.Layout
{
    public class UserMenuOrLoginLinkViewModel
    {
        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }
        public bool IsMultiTenancyEnabled { get; set; }

        public string GetShownLoginName()
        {
            var userName = "<span id=\"HeaderCurrentUserName\">" + LoginInformations.User.UserName + "</span>";

            if (!IsMultiTenancyEnabled)
            {
                return userName;
            }

            return LoginInformations.Tenant == null
                ? ".\\" + userName
                : LoginInformations.Tenant.TenancyName + "\\" + userName;
        }

        public string GetShownAvatarImage()
        {
            if (string.IsNullOrEmpty(LoginInformations.User.UrlImageAvatar))
            {
                return "<img src='/Content/img/avatars/sunny.png'/>";
            }
            else
            {
                return "<img src=" + LoginInformations.User.UrlImageAvatar + " />";
            }
        }
    }
}