var permissions = {};
permissions.ModuleZero = {};
permissions.ModuleZero.AdminMenu = "Administration_Menu";
permissions.ModuleZero.DashBoard = "DashBoard";
permissions.ModuleZero.Administration = "Administration";
permissions.ModuleZero.ManageUsers = "Administration.ManageUsers";
permissions.ModuleZero.ManageUsers_Create = "Administration.ManageUsers.Create";
permissions.ModuleZero.ManageUsers_Delete = "Administration.ManageUsers.Delete";
permissions.ModuleZero.ManageUsers_Edit = "Administration.ManageUsers.Edit";
permissions.ModuleZero.ManageUsers_ViewUsers = "Administration.ManageUsers.ViewUsers";
permissions.ModuleZero.ManageUsers_ChangeRoles = "Administration.ManageUsers.ChangeRoles";
permissions.ModuleZero.ManageRoles= "Administration.ManageRoles";
permissions.ModuleZero.ManageRoles_Create = "Administration.ManageRoles.Create";
permissions.ModuleZero.ManageRoles_Edit = "Administration.ManageRoles.Edit";
permissions.ModuleZero.ManageRoles_Delete = "Administration.ManageRoles.Delete";
permissions.ModuleZero.ManageUnits = "Administration.OrgUnits";
permissions.ModuleZero.ManageUnits_Create = "Administration.OrgUnits.Create";
permissions.ModuleZero.ManageUnits_Edit = "Administration.OrgUnits.Edit";
permissions.ModuleZero.ManageUnits_Delete = "Administration.OrgUnits.Delete";
permissions.ModuleZero.ManageUnits_Modify = "Administration.OrgUnits.Modify";
permissions.ModuleZero.ManageLanguages = "Administration.Languages";
permissions.ModuleZero.ManageLanguages_Create = "Administration.Languages.Create";
permissions.ModuleZero.ManageLanguages_Edit = "Administration.Languages.Edit";
permissions.ModuleZero.ManageLanguages_Texts = "Administration.Languages.Modify";
permissions.ModuleZero.ManageConfigs= "Administration.Configs";
//Shortcut to isGranted function
var isGranted = function(permissionConstant) {
    return abp.auth.isGranted(permissionConstant);
}
//Shortcut to permissions for ModuleZero
var zeroPermissions = permissions.ModuleZero;