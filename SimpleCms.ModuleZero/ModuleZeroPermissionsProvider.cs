using Abp.Authorization;
using Abp.Localization;
using SimpleCms.ModuleZero.Constants;

namespace SimpleCms.ModuleZero
{
    public class ModuleZeroPermissionsProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission("AdministrationMenu",
                new LocalizableString("Administration_Menu", ModuleZeroConstants.Source));
            context.CreatePermission("DashBoard",new LocalizableString("Administration_DashBoard",ModuleZeroConstants.Source));
            var administracion = context.CreatePermission("Administration",new LocalizableString("Administration",ModuleZeroConstants.Source),true);
            var manejoDeUsuarios = administracion.CreateChildPermission("Administration.ManageUsers", new LocalizableString("ManageUsers", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.Menu", new LocalizableString("ManageUsersMenu", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.Create", new LocalizableString("ManageUsersCreate", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.Delete", new LocalizableString("ManageUsersDelete", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.Edit", new LocalizableString("ManageUsersEdit", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.ViewUsers", new LocalizableString("ManageUsersViewUsers", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.ChangeRoles",new LocalizableString("ManageUsersChangeRoles",ModuleZeroConstants.Source));
            var manejoDeRoles = administracion.CreateChildPermission("Administration.ManageRoles", new LocalizableString("ManageRoles", ModuleZeroConstants.Source));
            manejoDeRoles.CreateChildPermission("Administration.ManageRoles.Menu", new LocalizableString("ManageRolesMenu", ModuleZeroConstants.Source));
            manejoDeRoles.CreateChildPermission("Administration.ManageRoles.Create", new LocalizableString("ManageRolesCreate", ModuleZeroConstants.Source));
            manejoDeRoles.CreateChildPermission("Administration.ManageRoles.Edit", new LocalizableString("ManageRolesEdit", ModuleZeroConstants.Source));
            manejoDeRoles.CreateChildPermission("Administration.ManageRoles.Delete", new LocalizableString("ManageDelete", ModuleZeroConstants.Source));
            var manejoDeUnidadesDeOrganizacion = administracion.CreateChildPermission("Administration.OrgUnits", new LocalizableString("OrgUnits", ModuleZeroConstants.Source));
            manejoDeUnidadesDeOrganizacion.CreateChildPermission("Administration.OrgUnits.Menu", new LocalizableString("OrgUnitsMenu", ModuleZeroConstants.Source));
            manejoDeUnidadesDeOrganizacion.CreateChildPermission("Administration.OrgUnits.Create", new LocalizableString("OrgUnitsCreate", ModuleZeroConstants.Source));
            manejoDeUnidadesDeOrganizacion.CreateChildPermission("Administration.OrgUnits.Edit", new LocalizableString("OrgUnitsEdit", ModuleZeroConstants.Source));
            manejoDeUnidadesDeOrganizacion.CreateChildPermission("Administration.OrgUnits.Delete", new LocalizableString("OrgUnitsDelete", ModuleZeroConstants.Source));
            manejoDeUnidadesDeOrganizacion.CreateChildPermission("Administration.OrgUnits.Modify", new LocalizableString("OrgUnitsModify", ModuleZeroConstants.Source));
            var manejoDeLenguages = administracion.CreateChildPermission("Administration.ManageLanguages", new LocalizableString("ManageLanguages", ModuleZeroConstants.Source));
            manejoDeLenguages.CreateChildPermission("Administration.ManageLanguages.Menu", new LocalizableString("ManageLanguagesMenu", ModuleZeroConstants.Source));
            manejoDeLenguages.CreateChildPermission("Administration.ManageLanguages.Create", new LocalizableString("ManageLanguagesCreate", ModuleZeroConstants.Source));
            manejoDeLenguages.CreateChildPermission("Administration.ManageLanguages.Edit", new LocalizableString("ManageLanguagesEdit", ModuleZeroConstants.Source));
            manejoDeLenguages.CreateChildPermission("Administration.ManageLanguages.Delete", new LocalizableString("ManageLanguagesDelete", ModuleZeroConstants.Source));
            manejoDeLenguages.CreateChildPermission("Administration.ManageLanguages.EditTexts", new LocalizableString("ManageLanguagesTexts", ModuleZeroConstants.Source));
            var manejoDeConfiguracion = administracion.CreateChildPermission("Administration.ManageConfiguration.Menu", new LocalizableString("ManageConfigurationMenu", ModuleZeroConstants.Source));
            manejoDeConfiguracion.CreateChildPermission("Administration.ManageConfiguration.Menu.Config", new LocalizableString("ManageConfigurationMenu.Delete", ModuleZeroConstants.Source));
        }
    }
}
