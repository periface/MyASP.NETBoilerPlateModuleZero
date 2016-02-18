using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.Users.Dto
{
    public class NewUserInput : IInputDto
    {
        public NewUserInput()
        {
                Roles = new List<RoleInput>();
        }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Email { get; set; }
        public string ImageAvatar { get; set; }
        //
        public bool CreateRandomPassword { get; set; }
        public bool SendActivationEmail { get; set; }
        //
        [DataType(DataType.Password)]
        public string PasswordInput { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        public bool IsActive { get; set; }
        public List<RoleInput> Roles {get;set;} 
        [Required]
        public string UserName { get; set; }
        public long Id { get; set; }
    }

    public class RoleInput
    {
        public string RoleName { get; set; }
        public bool Granted { get; set; }
    }
}
