using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleCms.Themes.Dto
{
    public class ThemeOutput : IOutputDto
    {
        public ThemeOutput()
        {
                Themes = new List<ThemeDto>();
        }
        public List<ThemeDto> Themes { get; set; }
    }

    public class ThemeDto
    {
        public int Id { get; set; }
        public int Uses { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public bool IsAdquired { get; set; }
        public bool StillInDevelopment { get; set; }
        public string UniqueFolderId { get; set; }
        public bool IsActive { get; set; }
    }
}
