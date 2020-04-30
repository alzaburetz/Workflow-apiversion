using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

using Newtonsoft.Json;


namespace Workflow.Models
{
    [Serializable]
    public class Comment
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("postid")]
        public string PostID { get; set; }
        [JsonProperty("author")]
        public UserModel Author { get; set; }
        [JsonProperty("at")]
        public UserModel At { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("created")]
        public long Created { get; set; }
        [JsonProperty("edited")]
        public long Edited { get; set; }
        [JsonProperty("likes")]
        public int Likes { get; set; }
    }
}
