using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using Workflow.ViewModels;

namespace Workflow.Models
{
    [Serializable]
    public class GroupModel:BaseViewModel
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
        public int UserCount
        {
            get => _userCount;
            set
            {
                _userCount = value;
                OnPropertyChanged(nameof(UserCount));
            }
        }
        private int _userCount;

        public GroupModel() { }
        public GroupModel(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
