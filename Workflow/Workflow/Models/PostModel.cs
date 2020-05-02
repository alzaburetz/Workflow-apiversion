using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

using Newtonsoft.Json;

using Workflow.ViewModels;

namespace Workflow.Models
{
    [Serializable]
    public class PostModel:ModelChanged
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("group_id")]
        public string GroupID { get; set; }
        [JsonProperty("author")]
        public UserModel Author { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("body")]
        public string Body { get; set; }
        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty("comments")]
        public List<Comment> Comments { get; set; }
        [JsonProperty("comments_count")]
        public int CommentCount { get; set; }
        [JsonIgnore]
        int _likes;
        [JsonProperty("likes")]
        public int Likes
        {
            get => _likes;
            set
            {
                _likes = value;
                OnPropertyChanged("Likes");
            }
        }
        [JsonIgnore]
        bool _liked;
        [JsonProperty("liked")]
        public bool Liked
        {
            get => _liked;
            set
            {
                _liked = value;
                OnPropertyChanged("Liked");
            }
        }
        [JsonProperty("tags")]
        public List<string> Tags { get; set; }
    }
}
