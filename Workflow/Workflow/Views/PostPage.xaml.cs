using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.ViewModels;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostPage : ContentPage
    {
        PostViewModel viewModel { get; set; }
        public PostPage()
        {
            InitializeComponent();
            Comment.TextChanged += (s, e) =>
            {
                if (e.NewTextValue.Length > 0)
                {
                    viewModel.CanSend = true;
                }
                else
                {
                    viewModel.CanSend = false;
                }
            };
            MessagingCenter.Subscribe<PostViewModel>(this, "ClearEntry", (s) => Comment.Text = "");
        }

        public PostPage(PostViewModel vm) : this()
        {
            BindingContext = viewModel = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.GetComments.Execute(null);
        }
    }
}