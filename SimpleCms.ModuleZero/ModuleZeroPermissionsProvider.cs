using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


            var administracion = context.CreatePermission("Administration",new LocalizableString("Administration",ModuleZeroConstants.Source));
            var manejoDeUsuarios = administracion.CreateChildPermission("Administration.ManageUsers", new LocalizableString("ManageUsers", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.Create", new LocalizableString("ManageUsersCreate", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.Delete", new LocalizableString("ManageUsersDelete", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.Edit", new LocalizableString("ManageUsersEdit", ModuleZeroConstants.Source));
            manejoDeUsuarios.CreateChildPermission("Administration.ManageUsers.ViewUsers", new LocalizableString("ManageUsersViewUsers", ModuleZeroConstants.Source));
            var manejoDeRoles = administracion.CreateChildPermission("Administration.ManageRoles", new LocalizableString("ManageRoles", ModuleZeroConstants.Source));
            manejoDeRoles.CreateChildPermission("Administration.ManageRoles.Create", new LocalizableString("ManageRolesCreate", ModuleZeroConstants.Source));
            manejoDeRoles.CreateChildPermission("Administration.ManageRoles.Edit", new LocalizableString("ManageRolesEdit", ModuleZeroConstants.Source));
            manejoDeRoles.CreateChildPermission("Administration.ManageRoles.Delete", new LocalizableString("ManageDelete", ModuleZeroConstants.Source));
            var manejoDeUnidadesDeOrganizacion = administracion.CreateChildPermission("Administration.OrgUnits", new LocalizableString("OrgUnits", ModuleZeroConstants.Source));
            manejoDeUnidadesDeOrganizacion.CreateChildPermission("Administration.OrgUnits.Create", new LocalizableString("OrgUnitsCreate", ModuleZeroConstants.Source));
            manejoDeUnidadesDeOrganizacion.CreateChildPermission("Administration.OrgUnits.Edit", new LocalizableString("OrgUnitsEdit", ModuleZeroConstants.Source));
            manejoDeUnidadesDeOrganizacion.CreateChildPermission("Administration.OrgUnits.Delete", new LocalizableString("OrgUnitsDelete", ModuleZeroConstants.Source));
            manejoDeUnidadesDeOrganizacion.CreateChildPermission("Administration.OrgUnits.Modify", new LocalizableString("OrgUnitsModify", ModuleZeroConstants.Source));
        }
    }
}
