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
globalVariables.Url.SiteConfig = "/AdminCms/SiteConfig/";
globalVariables.Url.Tags = "/AdminCms/Tags/";
globalVariables.Url.Themes = "/AdminCms/Themes/";

//Helper para realizar localización de textos
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

window.globalVariables = globalVariables;
window.globalConfigs = globalConfigs;
