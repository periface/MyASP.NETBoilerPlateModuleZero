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
    public class PagesService :ApplicationService, IPagesService
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
                ShortDescription = input.ShortDescription,
                Tags = GetTagsFromInput(input.Tags),
                Title = input.Title,
                FriendlyUrl = "",
                IsActive = false
            };
            var idPage = await _pagesManager.CreatePageAsync(page);
            return idPage;
        }

        public async Task AddContentToPageAsync(string content, int idPage)
        {
            var page = await _pagesManager.GetPage(idPage);
            page.CreateContent(content);
            await _pagesManager.UpdatePageAsync(page);

        }

        public async Task EditPage(PageInput input)
        {
            var page = await _pagesManager.GetPage(input.Id);
            page.Title = page.Title;
            page.ShortDescription = page.ShortDescription;
            page.Tags = GetTagsFromInput(input.Tags);
            page.FriendlyUrl = "";
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
                ShortDescription = page.ShortDescription,
                Title = page.Title,

            };
        }

        public async Task<PageContentInput> GetContentForEdit(int idPage)
        {
            var page = await _pagesManager.GetPage(idPage);
            return new PageContentInput()
            {
                IdPage = page.Id,
                Content = page.Content
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
