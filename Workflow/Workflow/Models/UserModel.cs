using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Workflow.Models
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("surname")]
        public string Surname { get; set; }
        [JsonProperty("workdays")]
        public int Workdays { get; set; }
        [JsonProperty("weekdays")]
        public int Weekdays { get; set; }
        [JsonProperty("firstwork")]
        public string FirstWork { get; set; }
        [JsonProperty("avatar")]
        public string Avatar { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("phone")]
        public string Phone { get; set; }
        [JsonProperty("groups")]
        public List<string> Groups { get; set; }
    }
}
