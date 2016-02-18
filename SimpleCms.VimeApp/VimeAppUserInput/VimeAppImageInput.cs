using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.VimeApp.VimeAppUserInput
{
    public class VimeAppImageInput : IInputDto
    {
        public Guid IdInfo { get; set; }
        public bool UseUniqueName { get; set; }
    }
}
