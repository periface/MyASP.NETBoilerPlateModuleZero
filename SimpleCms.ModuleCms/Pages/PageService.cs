using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleCms.ModuleCms.Pages.Dto;

namespace SimpleCms.ModuleCms.Pages
{
    public class PageService : IPageService
    {
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
    }
}
