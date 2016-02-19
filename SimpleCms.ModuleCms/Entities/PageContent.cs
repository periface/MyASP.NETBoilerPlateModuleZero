using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace SimpleCms.ModuleCms.Entities
{
    public class PageContent :FullAuditedEntity, IMustHaveTenant 
    {

        public string Title { get; set; }
        public string FriendlyUrl { get; set; }
        public string Lang { get; set; }
        public string ShortDescription { get; set; }
        public string Content { get; protected set; }
        public virtual void CreateContent(string content)
        {
            var rRemScript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            var rRemPhp = new Regex(@"<?php[^>]*>[\s\S]*? ?>");
            var output = rRemScript.Replace(content, "");
            var outPutNoPhp = rRemPhp.Replace(output, "");
            Content = outPutNoPhp;
        }
        public virtual Page Page { get; set; }
        public int TenantId { get; set; }
    }
}
