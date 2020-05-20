using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Workflow.Models
{
    [Serializable]
    public class SocialToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("has_logged_in")]
        public bool Logged { get; set; }
    }
}
