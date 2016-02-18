//El Peri!
var globalVariables = {};
globalVariables.Url = {};
globalVariables.Localization = {};


globalVariables.Localization.CmsModuleConstant = "ModuleCms";
globalVariables.Localization.ModuleZeroConstant = "ModuleZero";
globalVariables.Localization.EcommerceConstant = "Ecommerce";

globalVariables.Url.Users = "/Admin/Users/";
globalVariables.Url.Languages = "/Admin/Languages/";
globalVariables.Url.OrgUnits = "/Admin/OrgUnits/";
globalVariables.Url.Pages = "/Admin/Pages/";
globalVariables.Url.Roles = "/Admin/Roles/";
globalVariables.Url.SiteConfig = "/Admin/SiteConfig/";
globalVariables.Url.Tags = "/Admin/Tags/";
globalVariables.Url.Themes = "/Admin/Themes/";

//Helper para realizar localizaci�n de textos
var globalConfigs = function (constant) {
    globalConfigs.LocalizationConstant = constant || {};
    if (!constant) {
        console.error("Localization constant not defined!");
    }
    else {
        globalConfigs.LocalizationConstant = constant;
    }
    globalConfigs.L = function (string) {
        if (!globalConfigs.LocalizationConstant) {
            console.error("Localization constant not defined!");
        }
        else {
            var localize = abp.localization.localize || {};
            return localize(string, globalConfigs.LocalizationConstant);

        }
    };
    return globalConfigs;
};

Window.globalVariables = globalVariables;
window.globalConfigs = globalConfigs;
