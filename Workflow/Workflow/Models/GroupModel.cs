using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace Workflow.Models
{
    [Serializable]
    public class GroupModel
    {
        [JsonProperty("creator")]
        public UserModel Creator { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("usercount")]
        public int UserCount { get; set; }

        public GroupModel() { }
        public GroupModel(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
