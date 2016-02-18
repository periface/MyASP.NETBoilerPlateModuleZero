using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleCms.Inputs;

namespace SimpleCms.ModuleCms.Services
{
    public interface IPagesService : IApplicationService
    {
        Task<int> CreatePageAsync(PageInput input);
        Task AddContentToPageAsync(string content ,int idPage);
        Task EditPage(PageInput input);
        Task DeletePage(int idPage,bool deleteContent);
        Task ChangePageState(int idPage, bool state);
        Task AddRevisionToPage(string coments, int idPage);
        Task<PageInput> GetPageForEdit(int idPage);
        Task<PageContentInput> GetContentForEdit(int idPage);
    }
}
