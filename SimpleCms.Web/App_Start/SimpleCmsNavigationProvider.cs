using Abp.Application.Navigation;
using Abp.Localization;
using SimpleCms.Authorization;

namespace SimpleCms.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See Views/Layout/_TopMenu.cshtml file to know how to render menu.
    /// </summary>
    public class SimpleCmsNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        L("HomePage"),
                        url: "/",
                        icon: "fa fa-home",
                        order:1
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Themes",
                        L("Themes"),
                        url: "/Themes",
                        icon: "fa fa-paint-brush",
                        requiredPermissionName: PermissionNames.Pages_Tenants,
                        order: 98
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "About",
                        L("About"),
                        url: "/About",
                        icon: "fa fa-info",
                        order: 4
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Admin",
                        L("Admin_Mo"),
                        url: "/Admin/ControlPanel",
                        icon: "fa fa-info",
                        requiredPermissionName:"AdministrationMenu",
                        order: 97
                        )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SimpleCmsConsts.LocalizationSourceName);
        }
    }
}
