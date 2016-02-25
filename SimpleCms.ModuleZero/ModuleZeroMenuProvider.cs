using Abp.Application.Navigation;
using Abp.Localization;
using SimpleCms.Authorization;
using SimpleCms.ModuleZero.Constants;

namespace SimpleCms.ModuleZero
{
    public class ModuleZeroMenuProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.Menus.Add("ZeroMenu",
                new MenuDefinition(
                    "ZeroMenu",
                    L("Zero_Menu")).AddItem(
                    new MenuItemDefinition("ControlPanel",
                    new LocalizableString("Menu_ControlPanel", ModuleZeroConstants.Source),
                    "fa fa-lg fa-fw fa-bar-chart txt-color-blue",
                    "/Admin/ControlPanel",
                    requiresAuthentication: true,
                    requiredPermissionName: "DashBoard"
                    )
                    )
                .AddItem(new MenuItemDefinition(
                    "MyOrg",
                    L("MyOrg"),
                    icon: "fa fa-lg fa-fw fa-list-alt txt-color-blue",
                    requiresAuthentication: true).AddItem(
                    new MenuItemDefinition(
                    "OrgUnits",
                    L("OrgUnits"),
                    url: "/Admin/OrgUnits",
                    icon: "fa fa-list-alt",
                    requiresAuthentication: true,
                    requiredPermissionName: "Administration.OrgUnits.Menu"))

                .AddItem(
                    new MenuItemDefinition(
                        "Roles",
                    L("Roles"),
                    requiresAuthentication: true,
                     url: "/Admin/Roles",
                    requiredPermissionName: "Administration.ManageRoles.Menu",
                    icon: "fa fa-cube"))
                    .AddItem(
                    new MenuItemDefinition(
                        "Users",
                    L("Users"),
                    requiresAuthentication: true,
                    requiredPermissionName: "Administration.ManageUsers.Menu",
                    url: "/Admin/Users",
                    icon: "fa fa-users"))
                    .AddItem(
                    new MenuItemDefinition(
                        "Languages",
                    L("Languages"),
                    requiresAuthentication: true,
                    requiredPermissionName: "Administration.ManageLanguages.Menu",
                    url: "/Admin/Languages",
                    icon: "fa fa-flag"))
                    .AddItem(
                    new MenuItemDefinition(
                        "Config",
                    L("Config"),
                    requiresAuthentication: true,
                    requiredPermissionName: "Administration.ManageConfiguration.Menu",
                    url: "/Admin/Configuration",
                    icon: "fa fa-gear"))).AddItem(
                    new MenuItemDefinition(
                        "Tenants",
                        L("Tenants"),
                        url: "/Admin/Tenants",
                        icon: "fa fa-lg fa-fw fa-globe txt-color-blue",
                        requiredPermissionName: PermissionNames.Pages_Tenants,
                        order: 98
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Foda",
                        L("Foda_System"),
                        url: "/Admin/Foda",
                        requiresAuthentication: true,
                        icon: "fa fa-lg fa-fw fa fa-info txt-color-blue",
                        order:99
                        )
                )
                );

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ModuleZeroConstants.Source);
        }
    }
}
