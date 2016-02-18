using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleCms.Pages.Dto
{
    public class PagesWithCategoriesOutput : IOutputDto
    {
        public List<CategoriesDto> Categories { get; set; } 
    }

    public class CategoriesDto : IDto
    {
        public string Name { get; set; }
        public List<PrincipalPage> Pages { get; set; } 
    }

    public class PrincipalPage
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
