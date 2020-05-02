using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.ViewModels;
using Workflow.Models;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GroupPage : ContentPage
    {
        GroupPageViewModel viewModel { get; set; }
        public GroupPage(GroupPageViewModel vm)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
            MessagingCenter.Subscribe<GroupPageViewModel>(this, "ClearEntries", (s) =>
            {
                PostName.Text = "";
                Body.Text = "";
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadPosts.Execute(null);
        }

        void CreatePost(object sender, EventArgs args)
        {
            var post = new PostModel()
            {
                Name = PostName.Text,
                Body = Body.Text
            };

            viewModel.CreatePost.Execute(post);
        }

        async void OpenPost(object sender, EventArgs args)
        {
            var post_id = (sender as Image).ClassId;
            var post = viewModel.Posts.First(x => x.ID == post_id);
            await Navigation.PushAsync(new PostPage(new PostViewModel(post)));
        }
    }
}