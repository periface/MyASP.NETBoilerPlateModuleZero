using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleCms.Inputs
{
    public class PageInput : IInputDto
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public List<TagInput> Tags { get; set; }
        public int Id { get; set; }
    }

    public class TagInput
    {
        public int IdTag { get; set; }
        public string TagName { get; set; }
    }
}
