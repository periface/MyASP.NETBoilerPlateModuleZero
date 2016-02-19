using System.Linq;
using Abp.Application.Navigation;
using Abp.Localization;
using SimpleCms.ModuleCms.Constants;
using SimpleCms.ModuleCms.Pages;
using SimpleCms.ModuleCms.Services;

namespace SimpleCms.ModuleCms
{
    public class ModuleCmsMenuProvider : NavigationProvider
    {
        private readonly IPageService _pageService;

        public ModuleCmsMenuProvider(IPageService pageService)
        {
            _pageService = pageService;
        }


        public override void SetNavigation(INavigationProviderContext context)
        {
            var categories = _pageService.GetCategories();
            foreach (var model in categories.Categories)
            {
                var menu = new MenuItemDefinition(model.Name,
                    new LocalizableString(model.Name, ModuleCmsConstants.Source), url: "/Pages/" + model.Name,
                    order: 4, customData: "IsCategoriesMenu");
                    
                if (!model.Pages.Any()) continue;
                foreach (var principalPage in model.Pages)
                {
                    menu.AddItem(new MenuItemDefinition(principalPage.Name,new LocalizableString(principalPage.Name,ModuleCmsConstants.Source)));
                }
                context.Manager.MainMenu.AddItem(menu);
            }
            context.Manager.Menus["ZeroMenu"].
                AddItem(
                new MenuItemDefinition("Pages",
                new LocalizableString("Cms_System", ModuleCmsConstants.Source), "fa fa-lg fa-fw fa-bookmark txt-color-blue", customData: "CmsSystem")
                .AddItem(
                    new MenuItemDefinition(
                        name: "SiteConfig",
                        icon: "fa fa-desktop",
                        url: "/AdminCms/SiteConfig",
                        displayName: new LocalizableString("Cms_Config_General", ModuleCmsConstants.Source)
                        )).AddItem(
                    new MenuItemDefinition(
                        name: "Themes",
                        url: "/AdminCms/Themes",
                        icon: "fa fa-paint-brush",
                        displayName: new LocalizableString("Cms_Config_Theme", ModuleCmsConstants.Source)
                        )).AddItem(
                    new MenuItemDefinition(
                        name: "Pages",
                        url: "/AdminCms/Pages",
                        icon: "fa fa-file-text-o",
                        displayName: new LocalizableString("Cms_Pages", ModuleCmsConstants.Source)
                        )).AddItem(new MenuItemDefinition(
                            name: "Tags",
                            url: "/Admin/Tags",
                            icon: "fa fa-paragraph",
                        displayName: new LocalizableString("Cms_Tags", ModuleCmsConstants.Source)
                            )));
        }
    }
}
