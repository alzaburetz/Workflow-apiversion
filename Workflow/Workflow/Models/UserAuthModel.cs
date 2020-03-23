using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Workflow.Models
{
    public class UserAuthModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
