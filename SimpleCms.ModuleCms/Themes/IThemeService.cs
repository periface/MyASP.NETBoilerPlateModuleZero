using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleCms.Themes.Dto;

namespace SimpleCms.ModuleCms.Themes
{
    public interface IThemeService : IApplicationService
    {
        ThemeOutput GetStoreThemes();
        ThemeOutput GetTenantAsignedThemes();
        void AsignTheme(int idTheme);
        void AddComentToTheme(CommentThemeInput input);
        ThemeDto GetCurrentActiveThemeFromTenant();
        ThemeDto GetCurrentActiveThemeFromTenant(int idTenant);
        ThemeDto GetCurrentActiveThemeFromTenant(string tenancyName);
        void ActivateTheme(int idTheme);
        void GetTheme(int idTheme);
    }
}
