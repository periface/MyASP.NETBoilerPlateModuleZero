using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.Policies;

namespace SimpleCms.ModuleCms.Managers
{
    public class PagesManager : DomainService, IPagesManager
    {
        private readonly IRepository<Page,int> _pageRepository;
        private readonly IPagesOperationsPolicies _pagesCreationPolicy;
        public PagesManager(IRepository<Page,int> pageRepository, IPagesOperationsPolicies pagesCreationPolicy)
        {
            _pageRepository = pageRepository;
            _pagesCreationPolicy = pagesCreationPolicy;
        }

        public async Task<int> CreatePageAsync(Page input)
        {
            _pagesCreationPolicy.AttemptPageCreationAsync(input);
            var result = await _pageRepository.InsertAndGetIdAsync(input);
            return result;
        }
        
        public async Task<Page> GetPage(int idPage)
        {
            var page = await _pageRepository.FirstOrDefaultAsync(a => a.Id == idPage);
            if(page==null) throw new UserFriendlyException("Page not found");
            return page;
        }

        public async Task UpdatePageAsync(Page page)
        {
            if(!string.IsNullOrEmpty(page.Content)) _pagesCreationPolicy.AttemptAddContentToPageAsync(page);
            _pagesCreationPolicy.AttemptPageCreationAsync(page);
            await _pageRepository.InsertOrUpdateAsync(page);
        }

        public async Task<IEnumerable<Page>> GetPagesAsync(Expression<Func<Page, bool>> predicate)
        {
            var pages = await _pageRepository.GetAllListAsync(predicate);

            return pages;
        }

        public async Task DeletePageAsync(Page page)
        {
            await _pageRepository.DeleteAsync(page);
        }
    }
}
