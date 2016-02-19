using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleCms.Pages.Dto
{
    public class InitialCategoryInput : IInputDto
    {
        public string Lang { get; set; }
        public string Title { get; set; }
    }
}
