﻿using System;
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
        public GroupPage(GroupPageViewModel vm, bool createMenu = false)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
            MessagingCenter.Subscribe<GroupPageViewModel>(this, "ClearEntries", (s) =>
            {
                PostName.Text = "";
                Body.Text = "";
            });

            MessagingCenter.Subscribe<GroupsPage>(this, "LoadPosts", (sender) => viewModel.LoadPosts.Execute(null));
            Body.TextChanged += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.NewTextValue))
                {
                    if (args.NewTextValue[args.NewTextValue.Length - 1] == ' ' && args.NewTextValue.Contains('#'))
                    {
                        int hash_index = args.NewTextValue.IndexOf('#');
                        var tag = args.NewTextValue.Substring(hash_index);
                        Device.BeginInvokeOnMainThread(() => viewModel.Tags.Add(tag.Trim()));
                        Body.Text = Body.Text.Remove(hash_index);
                    }
                }
            };

            if (createMenu)
            {
                this.ToolbarItems.Add(new ToolbarItem("Удалить", "", async () =>
                  {
                      var shouldDelete = await DisplayAlert(null, "Удалить группу", "Да", "Нет");
                      if (shouldDelete)
                      {
                          viewModel.DeleteGroup.Execute(null);
                          MessagingCenter.Send<GroupPage, string>(this, "RemoveGroup", viewModel.Group.ID);
                          await Navigation.PopAsync();
                      }
                  }, ToolbarItemOrder.Secondary));

                this.ToolbarItems.Add(new ToolbarItem("Редактировать", "", async () =>
                {
                    await Navigation.PushAsync(new CreateGroupPage(this.Navigation, viewModel.Group));
                }, ToolbarItemOrder.Secondary));
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        async void CreatePost(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(Body.Text) && !string.IsNullOrEmpty(PostName.Text))
            {
                var post = new PostModel()
                {
                    Name = PostName.Text,
                    Body = Body.Text
                };

                viewModel.CreatePost.Execute(post);
            }
            else
            {
                await DisplayAlert(null, "Введите верные данные. Тело и название поста не должно быть пустым", "OK");
            }
        }

        async void OpenPost(object sender, EventArgs args)
        {
            var post_id = (sender as Image).ClassId;
            var post = viewModel.Posts.First(x => x.ID == post_id);
            await Navigation.PushAsync(new PostPage(new PostViewModel(post)));
        }
    }
}