using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.UserInput
{
    public class DeleteUnitInputDto : IInputDto
    {
        public int Id { get; set; }
    }
}
