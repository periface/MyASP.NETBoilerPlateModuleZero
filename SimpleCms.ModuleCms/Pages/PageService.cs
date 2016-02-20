using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Dependency;
using SimpleCms.ModuleCms.Entities;
using SimpleCms.ModuleCms.Managers;
using SimpleCms.ModuleCms.Pages.Dto;

namespace SimpleCms.ModuleCms.Pages
{
    public class PageService : SimpleCmsAppServiceBase, IPageService 
    {
        private readonly IPagesManager _pagesManager;

        public PageService(IPagesManager pagesManager)
        {
            _pagesManager = pagesManager;
        }

        public PagesWithCategoriesOutput GetCategories()
        {
            return new PagesWithCategoriesOutput()
            {
                Categories = new List<CategoriesDto>()
                {
                    new CategoriesDto()
                    {
                        Name = "Sports",
                        Pages = new List<PrincipalPage>()
                        {
                            new PrincipalPage()
                            {
                                Name = "Yet another sports post!",
                                Url = "/Page/Name/Yet-another-sports-post!"
                            },
                             new PrincipalPage()
                            {
                                Name = "sports post!",
                                Url = "/Page/Name/Yet-another-sports-post!"
                            },
                              new PrincipalPage()
                            {
                                Name = "post!",
                                Url = "/Page/Name/Yet-another-sports-post!"
                            }
                        }
                    },
                    new CategoriesDto()
                    {
                        Name = "Healt",
                        Pages = new List<PrincipalPage>()
                        {
                            new PrincipalPage()
                            {
                                Name = "Yet another health post!",
                                Url = "/Page/Name/Yet-another-health-post!"
                            },
                            new PrincipalPage()
                            {
                                Name = "health post!",
                                Url = "/Page/Name/Yet-another-health-post!"
                            },
                            new PrincipalPage()
                            {
                                Name = "post!",
                                Url = "/Page/Name/Yet-another-health-post!"
                            }
                        }
                    }
                },

            };
        }

        public CategoryOutput GetOnlyCategories()
        {
            var categories = _pagesManager.GetCategories();
            var lang = CultureInfo.CurrentCulture;
            return new CategoryOutput()
            {
                Categories = categories.Select(a =>
                {
                    var singleOrDefault = a.Content.SingleOrDefault(c => c.Lang == lang.Name);
                    return singleOrDefault != null ? new CategoriesDto()
                    {
                        Id = singleOrDefault.Id,
                        Name = singleOrDefault.CategoryName,
                    } : null;
                }).ToList()
            };
        }

        public async Task CreateCategory(InitialCategoryInput input)
        {
            var category = new PageCategory()
            {
                Content = new List<CategoryContent>()
                {
                    new CategoryContent()
                    {
                        Lang = input.Lang,
                        CategoryName = input.Title
                    }
                }
            };
            await _pagesManager.CreateCategory(category);
        }

        public async Task CreatePage(InitialPageInput input)
        {
            var page = new Page()
            {
                PageCategory = new PageCategory()
                {
                    Id = input.PageCategoryId
                },
                Content = new List<PageContent>()
                {
                    new PageContent()
                    {
                        Lang = input.Lang,
                        ShortDescription = input.ShortDescription,
                        Title = input.Title,
                    }
                }
            };
            await _pagesManager.CreatePageAsync(page);
        }

        public Task AddContent()
        {
            throw new NotImplementedException();
        }

        public CategoryOutput GetOnlyCategories(string langName)
        {
            if (string.IsNullOrEmpty(langName))
            {
                langName = CultureInfo.CurrentCulture.Name;
            }
            var categories = _pagesManager.GetCategories();
            return new CategoryOutput()
            {
                Categories = categories.Select(a =>
                {
                    var singleOrDefault = a.Content.SingleOrDefault(c => c.Lang == langName && c.Category.Id == a.Id);
                    return singleOrDefault != null ? new CategoriesDto()
                    {
                        Id = singleOrDefault.Id,
                        Name = singleOrDefault.CategoryName,
                    } : null;
                }).ToList()
            };
        }
    }
}
