﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.UserInput
{
    public class UserToUnitInput : IInputDto
    {
        public int IdUser { get; set; }
        public long IdUnit { get; set; }
    }
}
