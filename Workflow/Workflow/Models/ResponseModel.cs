using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Workflow.Models
{
    public class ResponseModel<T>
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("errors")]
        public List<string> Errors { get; set; }
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}
