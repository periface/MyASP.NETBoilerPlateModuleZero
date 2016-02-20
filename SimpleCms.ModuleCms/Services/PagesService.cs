using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.Inputs;
using SimpleCms.ModuleCms.Managers;

namespace SimpleCms.ModuleCms.Services
{
    public class PagesService : SimpleCmsAppServiceBase, IPagesService
    {
        private readonly IPagesManager _pagesManager;

        public PagesService(IPagesManager pagesManager)
        {
            _pagesManager = pagesManager;
        }

        public async Task<int> CreatePageAsync(PageInput input)
        {
            var page = new Page()
            {
                Tags = GetTagsFromInput(input.Tags),
                IsActive = false
            };
            var idPage = await _pagesManager.CreatePageAsync(page);
            return idPage;
        }

        public async Task AddContentToPageAsync(string content, int idPage)
        {
            var page = await _pagesManager.GetPage(idPage);
            await _pagesManager.UpdatePageAsync(page);

        }

        public async Task EditPage(PageInput input)
        {
            var page = await _pagesManager.GetPage(input.Id);
            page.Tags = GetTagsFromInput(input.Tags);
            await _pagesManager.UpdatePageAsync(page);
        }

        public async Task DeletePage(int idPage, bool deleteContent)
        {
            var page = await _pagesManager.GetPage(idPage);
            if (deleteContent)
            {
                //Todo: Delete Content (Images)
            }
            await _pagesManager.DeletePageAsync(page);
        }

        public async Task ChangePageState(int idPage,bool state)
        {
            var page = await _pagesManager.GetPage(idPage);
            page.IsActive = state;
            await _pagesManager.UpdatePageAsync(page);
        }

        public Task AddRevisionToPage(string coments, int idPage)
        {
            throw new NotImplementedException();
        }

        public async Task<PageInput> GetPageForEdit(int idPage)
        {
            var page = await _pagesManager.GetPage(idPage);
            return new PageInput()
            {
                Id = page.Id,

            };
        }

        public async Task<PageContentInput> GetContentForEdit(int idPage)
        {
            var page = await _pagesManager.GetPage(idPage);
            return new PageContentInput()
            {
                IdPage = page.Id,
            };
        }

        private ICollection<PageTags> GetTagsFromInput(IEnumerable<TagInput> tags)
        {
            return tags.Select(tagInput => new PageTags()
            {
                Id = tagInput.IdTag,
                Tag = tagInput.TagName
            }).ToList();
        }
    }
}
