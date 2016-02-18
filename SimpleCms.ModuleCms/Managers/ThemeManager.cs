using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.SiteConfiguration;

namespace SimpleCms.ModuleCms.Managers
{
    public class ThemeManager : IThemeManager
    {
        private readonly IRepository<Theme> _themeRepository;
        private readonly IRepository<ConfigThemeRelation> _themeRelationRepository;
        public ThemeManager(IRepository<Theme> themeRepository, IRepository<ConfigThemeRelation> themeRelationRepository)
        {
            _themeRepository = themeRepository;
            _themeRelationRepository = themeRelationRepository;
        }

        public async Task<IEnumerable<Theme>> GetAvailableThemesAsync()
        {
            return await _themeRepository.GetAllListAsync(a => a.IsAvailable);
        }

        public IEnumerable<Theme> GetAvailableThemes()
        {
            return _themeRepository.GetAllList(a => a.IsAvailable);
        }

        public async Task<IEnumerable<Theme>> GetAllThemes()
        {
            return await _themeRepository.GetAllListAsync();
        }

        public async Task<IEnumerable<Theme>> GetAllThemes(Expression<Func<Theme, bool>> delegateExpression)
        {
            var themes = await _themeRepository.GetAllListAsync(delegateExpression);
            return themes;
        }

        public Theme GetTheme(int idTheme)
        {
            return _themeRepository.Get(idTheme);
        }

        public IEnumerable<Theme> GetThemesFromConfig(int idConfig)
        {
            var themes = _themeRelationRepository.GetAllList(a=>a.IdConfig == idConfig);
            return themes.Select(theme => _themeRepository.Get(theme.IdTheme)).ToList();
        }


        public void CreateNewTheme(Theme theme)
        {
            throw new NotImplementedException();
        }
    }
}
