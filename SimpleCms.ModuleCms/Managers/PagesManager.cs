using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Localization;
using Abp.UI;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.Pages.Dto;
using SimpleCms.ModuleCms.Policies;

namespace SimpleCms.ModuleCms.Managers
{
    public class PagesManager : DomainService, IPagesManager
    {
        private readonly IRepository<Page,int> _pageRepository;
        private readonly IRepository<PageContent, int> _pageContentRepository;
        private readonly IPagesOperationsPolicies _pagesCreationPolicy;
        private readonly IRepository<PageCategory, int> _categoryRepository;
        public PagesManager(IRepository<Page,int> pageRepository, IPagesOperationsPolicies pagesCreationPolicy, IRepository<PageContent, int> pageContentRepository, IRepository<PageCategory, int> categoryRepository)
        {
            _pageRepository = pageRepository;
            _pagesCreationPolicy = pagesCreationPolicy;
            _pageContentRepository = pageContentRepository;
            _categoryRepository = categoryRepository;
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

            if(!string.IsNullOrEmpty(page.Content.First().Content)) _pagesCreationPolicy.AttemptAddContentToPageAsync(page);
            _pagesCreationPolicy.AttemptPageCreationAsync(page);
            await _pageRepository.InsertOrUpdateAsync(page);
        }

        public async Task CreateCategory(PageCategory input)
        {
           
            await _categoryRepository.InsertAsync(input);
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
        public async Task UpdatePageContentAsync(PageContent content, int pageId, string language)
        {
            var page = _pageRepository.Get(pageId);
            var pageContent = _pageContentRepository.FirstOrDefault(a => a.Lang == language && a.Page.Id == page.Id);
            if (pageContent == null)
            {
                content.Page = page;
                await _pageContentRepository.InsertAsync(content);
            }
            else
            {
                pageContent.Title = content.Title;
                pageContent.CreateContent(content.Content);
                pageContent.ShortDescription = content.ShortDescription;
                await _pageContentRepository.UpdateAsync(pageContent);
            }
        }

        public IEnumerable<PageCategory> GetCategories()
        {
            return _categoryRepository.GetAllList();
        }
    }
}
