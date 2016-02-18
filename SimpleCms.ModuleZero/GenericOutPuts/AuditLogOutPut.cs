using Abp.Application.Services.Dto;

namespace SimpleCms.ModuleZero.GenericOutPuts
{
    public class AuditLogOutPut : IOutputDto
    {
        public long Id { get; set; }
        public string Time { get; set; }
        public string Service { get; set; }
        public string Action { get; set; }
        public string Duration { get; set; }
        public string IpAdress { get; set; }
        public string Browser { get; set; }
        public string UserName { get; set; }
    }
}
