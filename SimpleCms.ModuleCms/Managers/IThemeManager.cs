using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Services;
using SimpleCms.ModuleCms.Entities;

namespace SimpleCms.ModuleCms.Managers
{
    public interface IThemeManager : IDomainService
    {
        Task<IEnumerable<Theme>> GetAvailableThemesAsync();
        IEnumerable<Theme> GetAvailableThemes();
        Task<IEnumerable<Theme>> GetAllThemes();
        Task<IEnumerable<Theme>> GetAllThemes(Expression<Func<Theme,bool>>delegateExpression);
        Theme GetTheme(int idTheme);
        IEnumerable<Theme> GetThemesFromConfig(int idConfig); 
        void CreateNewTheme(Theme theme);
    }
}
