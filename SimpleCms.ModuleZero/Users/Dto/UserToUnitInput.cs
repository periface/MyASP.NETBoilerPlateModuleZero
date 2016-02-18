using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.Users.Dto
{
    public class UserToUnitInput : IInputDto
    {
        public int IdUser { get; set; }
        public long IdUnit { get; set; }
    }
}
