using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Navigation;
using Abp.Localization;

namespace SimpleCms.VimeApp
{
    public class ModuleMenuProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            var menu = new MenuDefinition("VimeMainMenu",
                new LocalizableString("menuAdmin", SimpleCmsVimeAppConstants.LocalizationSourceName))
                .AddItem(
                    new MenuItemDefinition(
                        "Info_item",
                        new LocalizableString("info_admin", "VimeApp"),
                        url: "/Admin/Info/Index",
                        icon: "fa fa-tasks"
                        ).AddItem(new MenuItemDefinition(
                        "Info_item_create",
                        new LocalizableString("BtnCreate", "VimeApp"),
                        url: "/Admin/Info/CreateInfo",
                        icon: "fa fa-tasks"
                        ))
                ).AddItem(new MenuItemDefinition(
                    "Landing_item",
                    new LocalizableString("landing_admin", "VimeApp"),
                    url: "/Admin/LandingPage/Index",
                    icon: "fa fa-tasks"
                    ).AddItem(new MenuItemDefinition(
                        "Landing_createItem",
                        new LocalizableString("landing_create_admin", "VimeApp"),
                        icon: "fa fa-tasks"
                        )));
            context.Manager.Menus.Add("VimeMainMenu", menu);
            context.Manager.MainMenu.AddItem(new MenuItemDefinition("Administracion",
                new LocalizableString(
                    "Admin_Module_Vime",
                    SimpleCmsVimeAppConstants.LocalizationSourceName), 
                url: "/Admin/ControlPanel",requiresAuthentication:true,order:5));
        }
    }
}
