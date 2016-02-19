using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Dependency;
using SimpleCms.ModuleCms.Pages.Dto;

namespace SimpleCms.ModuleCms.Pages
{
    public interface IPageService : IApplicationService
    {
        PagesWithCategoriesOutput GetCategories();
        CategoryOutput GetOnlyCategories();
        Task CreateCategory(InitialCategoryInput input);
        Task CreatePage(InitialPageInput input);
        Task AddContent();
        CategoryOutput GetOnlyCategories(string langName);
    }
}
