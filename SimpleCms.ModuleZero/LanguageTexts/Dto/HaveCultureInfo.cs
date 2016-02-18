using System.Globalization;

namespace SimpleCms.ModuleZero.LanguageTexts.Dto
{
    public abstract class HaveCultureInfo : HaveBaseLanguagePetition
    {
        public string CultureInfoName { get; set; }
        public CultureInfo Culture => SetCultureInfo(CultureInfoName);
        private static CultureInfo SetCultureInfo(string cultureInfo)
        {
            return new CultureInfo(cultureInfo);
        }
    }

    public class HaveBaseLanguagePetition
    {
        public string BaseCultureInfoName { get; set; }
        
    }
}
