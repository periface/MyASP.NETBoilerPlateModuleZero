using Abp.Application.Navigation;
using Abp.Localization;
using SimpleCms.ModuleEcommerce.Constants;

namespace SimpleCms.ModuleEcommerce
{
    public class ModuleEcommerceMenuProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.Menus["ZeroMenu"].AddItem(
                new MenuItemDefinition("Ecommerce",
                new LocalizableString("Ecom_System", ModuleEcommerceConstants.Source), "fa fa-lg fa-fw fa-cart-plus txt-color-blue", customData: "EcommerceSystem")
                .AddItem(
                    new MenuItemDefinition(
                        name: "Eco_Reports",
                        icon: "fa fa-book",
                        displayName: new LocalizableString("Eco_Reports", ModuleEcommerceConstants.Source)
                        ))
                        .AddItem(new MenuItemDefinition(
                            name: "Eco_Categ",
                            icon: "fa fa-folder",
                            displayName: new LocalizableString("Eco_Categories", ModuleEcommerceConstants.Source)
                            ))
                       .AddItem(new MenuItemDefinition(
                            name: "Eco_Products",
                            icon: "fa fa-shopping-basket",
                             displayName: new LocalizableString("Eco_Products", ModuleEcommerceConstants.Source)
                            ))
                       .AddItem(new MenuItemDefinition(
                            name: "Eco_Ship",
                            icon: "fa fa-bus",
                             displayName: new LocalizableString("Eco_Shipping", ModuleEcommerceConstants.Source)
                            ))
                      .AddItem(new MenuItemDefinition(
                            name: "Eco_Carts",
                            icon: "fa fa-shopping-bag",
                            displayName: new LocalizableString("Eco_Carts", ModuleEcommerceConstants.Source)
                            ))
                      .AddItem(new MenuItemDefinition(
                            name: "Eco_PayPal",
                            icon: "fa fa-cc-paypal",
                            displayName: new LocalizableString("Eco_PayPal", ModuleEcommerceConstants.Source)
                            )));
        }
    }
}
