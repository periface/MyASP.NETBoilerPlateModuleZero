using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleCms.Inputs
{
    public class PageContentInput : IInputDto
    {
        public int IdPage { get; set; }
        [AllowHtml]
        public string Content { get; set; }
    }
}
