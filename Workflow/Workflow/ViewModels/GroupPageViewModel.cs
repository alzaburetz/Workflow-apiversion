using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using Workflow.Models;
using Xamarin.Forms;

using System.Threading.Tasks;
using System.Security;
using System.Linq;

namespace Workflow.ViewModels
{
    public class GroupPageViewModel:BaseViewModel
    {
        GroupModel _group;
        public GroupModel Group
        {
            get => _group;
            set
            {
                _group = value;
                OnPropertyChanged("Group");
            }
        }
        public ObservableCollection<PostModel> Posts { get; set; }
        public ObservableCollection<string> Tags { get; set; }
        public Command LoadPosts { get; set; }
        public Command CreatePost { get; set; }
        public Command LikePost { get; set; }
        public Command EnterOrLeaveGroup { get; set; }
        public Command DeleteGroup { get; set; }
        string _entergrouptext;
        public string EnterGroupText
        {
            get => _entergrouptext;
            set
            {
                _entergrouptext = value;
                OnPropertyChanged(nameof(EnterGroupText));
            }
        }
        bool _canEnter;
        bool CanEnter
        {
            get => _canEnter;
            set
            {
                _canEnter = value;
                if (_canEnter)
                {
                    EnterGroupText = "Войти в группу";
                }
                else
                {
                    EnterGroupText = "Выйти из группы";
                }
            }
        }
        public GroupPageViewModel(GroupModel group, UserModel User)
        {
            Group = group;
            Tags = new ObservableCollection<string>();
            MessagingCenter.Subscribe<CreateGroupViewModel, GroupModel>(this, "UpdateGroup", (sender, gr) =>
            {
                this.Group = gr;
            });
            LikePost = new Command<PostModel>((post) =>
            {
                post.Liked = !post.Liked;
                if (post.Liked)
                {
                    post.Likes++;
                }
                else
                {
                    post.Likes--;
                }
                Task.Run(async () =>
                {
                    await HttpService.PutRequest<string, PostModel>($"groups/{Group.ID}/posts/{post.ID}/like", post);
                });
            });
            Posts = new ObservableCollection<PostModel>();
            LoadPosts = new Command(() =>
            {
                IsBusy = true;
                Task.Run(async () =>
                {
                    var resp = await HttpService.GetRequest<ResponseModel<List<PostModel>>>($"groups/{Group.ID}/posts");
                    if (resp.Code == 200)
                    {
                        if (resp.Response.Count > 0)
                        foreach (var post in resp.Response)
                        {
                            Device.BeginInvokeOnMainThread(() => Posts.Add(post));
                        }
                    }
                });
                IsBusy = false;
            });
            if (User.Groups != null)
            {
                CanEnter = !User.Groups.Contains(Group.ID);
            }
            CreatePost = new Command<PostModel>(async (p) =>
            {
                if (Tags.Count > 0)
                {
                    p.Tags.AddRange(Tags.ToList());
                }
                var resp = await HttpService.PostRequest<ResponseModel<PostModel>, PostModel>($"groups/{Group.ID}/posts/add", p, true);
                if (resp.Code == 200)
                {
                    //
                    //Posts.Add(resp.Response);
                    Posts.Insert(0, resp.Response);
                    MessagingCenter.Send<GroupPageViewModel>(this, "ClearEntries");
                }
            });
            EnterOrLeaveGroup = new Command(() =>
            {
                IsBusy = true;
                Task.Run(async () =>
                {
                    if (CanEnter)
                    {
                        var a = await HttpService.PostRequest<ResponseModel<string>, Object>($"groups/{Group.ID}/enter", null, true);
                    }
                    else
                    {
                        var resp = await HttpService.PostRequest<ResponseModel<string>, Object>($"groups/{Group.ID}/exit", null, true);
                    }
                    CanEnter = !CanEnter;
                    IsBusy = false;
                });
            });
            DeleteGroup = new Command(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1.0f));
            });
        }
    }
}
