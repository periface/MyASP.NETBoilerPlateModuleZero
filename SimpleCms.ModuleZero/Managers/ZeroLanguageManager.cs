using System.Globalization;
using System.Threading.Tasks;
using Abp.Domain.Services;
using Abp.Localization;
using SimpleCms.ModuleZero.Constants;

namespace SimpleCms.ModuleZero.Managers
{
    public class ZeroLanguageManager : DomainService, IZeroLanguageManager
    {
        private readonly IApplicationLanguageManager _applicationLanguageManager;
        private readonly IApplicationLanguageTextManager _applicationLanguageTextManager;

        public ZeroLanguageManager(IApplicationLanguageManager applicationLanguageManager, IApplicationLanguageTextManager applicationLanguageTextManager)
        {
            _applicationLanguageManager = applicationLanguageManager;
            _applicationLanguageTextManager = applicationLanguageTextManager;
        }

        public async Task CreateLang()
        {
            await _applicationLanguageTextManager.UpdateStringAsync(2, ModuleZeroConstants.Source,new CultureInfo("en"), "name", "MiName");

        }
    }
}