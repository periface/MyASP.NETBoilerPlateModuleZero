using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GoogleRecaptchaDotNetMvc.Models
{
    public class ResultModel
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes { get; set; }
    }

    public class ErrorCodes
    {
        public const string MissigInputSecret = "missing-input-secret";
        public const string InvalidInputSecret = "invalid-input-secret";
        public const string InvalidInputReponse = "invalid-input-response";
    }
}
