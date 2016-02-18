using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleCms.SiteConfiguration.Dto
{
    public class ConfigsOutput : IOutputDto
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; }
        public bool AllowUsersRegistration { get; set; }
        public int ThemesCount { get; set; }
    }
}
