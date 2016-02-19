using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleCms.Pages.Dto
{
    public class InitialPageInput : IInputDto
    {
        public string Title { get; set; }
        public string Lang  { get; set; }
        public string ShortDescription { get; set; }
        public int PageCategoryId { get; set; }
                  
    }
}
