using System.Collections.Generic;
using System.Text.RegularExpressions;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class Page : FullAuditedEntity<int> , IMustHaveTenant
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; protected set; }
        public int TenantId { get; set; }
        public string FriendlyUrl { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<PageTags> Tags { get; set; }
        public virtual PageCategory PageCategory { get; set; }
        public virtual void CreateContent(string content)
        {
            var rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            var rRemPhp = new Regex(@"<?php[^>]*>[\s\S]*? ?>");
            var output = rRemScript.Replace(content, "");
            var outPutNoPhp = rRemPhp.Replace(output, "");
            Content = outPutNoPhp;
        }
    }
}
