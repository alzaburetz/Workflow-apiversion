using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using Workflow.Models;
using Xamarin.Forms;

using System.Threading.Tasks;
using System.Security;

namespace Workflow.ViewModels
{
    public class GroupPageViewModel:BaseViewModel
    {
        public GroupModel Group { get; set; }
        public ObservableCollection<PostModel> Posts { get; set; }
        public Command LoadPosts { get; set; }
        public Command CreatePost { get; set; }
        public Command LikePost { get; set; }
        public GroupPageViewModel(GroupModel group)
        {
            Group = group;
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

            CreatePost = new Command<PostModel>(async (p) =>
            {
                var resp = await HttpService.PostRequest<ResponseModel<PostModel>, PostModel>($"groups/{Group.ID}/posts/add", p, true);
                if (resp.Code == 200)
                {
                    //
                    //Posts.Add(resp.Response);
                    Posts.Insert(0, resp.Response);
                    MessagingCenter.Send<GroupPageViewModel>(this, "ClearEntries");
                }
            });
        }
    }
}
