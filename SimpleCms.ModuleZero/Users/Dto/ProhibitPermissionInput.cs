using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.Users.Dto
{
    public class ProhibitPermissionInput : IInputDto
    {
        public int UserId { get; set; }
        public string PermissionName { get; set; }
    }
}
