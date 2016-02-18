using Abp.Application.Navigation;
using Abp.Localization;
using SimpleCms.ModuleZero.Constants;

namespace SimpleCms.ModuleZero
{
    public class ModuleZeroMenuProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            //context.Manager.Menus.Add("ZeroMenu",new MenuDefinition("ZeroMainMenu",new LocalizableString("Menu","")).AddItem(new MenuItemDefinition(
            //    "Roles_Menu",
            //    new LocalizableString()
            //    )));
            context.Manager.Menus.Add("ZeroMenu",
                new MenuDefinition(
                    "ZeroMenu",
                    L("Zero_Menu")).AddItem(
                    new MenuItemDefinition("ControlPanel",
                    new LocalizableString("Control_Panel", ModuleZeroConstants.Source),
                    "fa fa-lg fa-fw fa-bar-chart txt-color-blue",
                    "/Admin/ControlPanel",
                    requiresAuthentication: true
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
                    requiredPermissionName: "Administration.OrgUnits"))

                .AddItem(
                    new MenuItemDefinition(
                        "Roles",
                    L("Roles"),
                    requiresAuthentication: true,
                     url: "/Admin/Roles",
                    requiredPermissionName: "Administration.ManageRoles",
                    icon: "fa fa-cube"))
                    .AddItem(
                    new MenuItemDefinition(
                        "Users",
                    L("Users"),
                    requiresAuthentication: true,
                    requiredPermissionName: "Administration.ManageUsers",
                    url: "/Admin/Users",
                    icon: "fa fa-users"))
                    .AddItem(
                    new MenuItemDefinition(
                        "Languages",
                    L("Languages"),
                    requiresAuthentication: true,
                    url: "/Admin/Languages",
                    icon: "fa fa-flag"))
                    .AddItem(
                    new MenuItemDefinition(
                        "Config",
                    L("Config"),
                    requiresAuthentication: true,
                    url: "/Admin/Users",
                    icon: "fa fa-gear")))
                );

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ModuleZeroConstants.Source);
        }
    }
}
