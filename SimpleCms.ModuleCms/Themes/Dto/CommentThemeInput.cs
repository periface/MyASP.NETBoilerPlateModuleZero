using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleCms.Themes.Dto
{
    public class CommentThemeInput : IInputDto
    {
        [Required]
        public string Comment { get; set; }
        public int IdTheme { get; set; }
    }
}
