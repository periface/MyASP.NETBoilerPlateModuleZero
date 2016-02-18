using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using SimpleCms.ModuleCms.Pages.Dto;

namespace SimpleCms.ModuleCms.Pages
{
    public interface IPageService : IApplicationService
    {
       PagesWithCategoriesOutput GetCategories();
    }
}
