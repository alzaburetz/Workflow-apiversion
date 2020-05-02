using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Workflow.Models;
using Xamarin.Forms;

using System.Threading.Tasks;

namespace Workflow.ViewModels
{
    public class PostViewModel:BaseViewModel
    {
        PostModel _post;
        public PostModel Post
        {
            get => _post;
            set
            {
                _post = value;
                OnPropertyChanged("Post");
            }
        }

        public ObservableCollection<Comment> Comments { get; set; }
        public Command GetComments { get; set; }
        public Command SendComment { get; set; }
        bool _cansend;
        public bool CanSend
        {
            get => _cansend;
            set
            {
                _cansend = value;
                OnPropertyChanged("CanSend");
            }
        }
        public PostViewModel(PostModel Post)
        {
            this.Post = Post;
            Comments = new ObservableCollection<Comment>();
            GetComments = new Command(() =>
            {
                IsBusy = true;
                Task.Run(async () =>
                {
                    var resp = await HttpService.GetRequest<ResponseModel<List<Comment>>>($"groups/{Post.GroupID}/posts/{Post.ID}/comments");
                    if (resp.Response != null)
                    {
                        foreach (var comment in resp.Response)
                        {
                            Device.BeginInvokeOnMainThread(() => Comments.Add(comment));
                        }
                    }
                    IsBusy = false;
                });
            });

            SendComment = new Command<string>((comment) =>
            {
                Task.Run(async () =>
                {
                    var Comment = new Comment() { Body = comment };
                    var resp = await HttpService.PostRequest<ResponseModel<Comment>, Comment>($"groups/{Post.GroupID}/posts/{Post.ID}/comments/add", Comment, true);
                    if (resp.Code == 200)
                    {
                        Device.BeginInvokeOnMainThread(() => Comments.Add(resp.Response));
                        MessagingCenter.Send<PostViewModel>(this, "ClearEntry");
                    }
                });
            });
        }
    }
}
