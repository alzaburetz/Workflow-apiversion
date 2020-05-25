using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Workflow.ViewModels;
using Workflow.Models;
using System.Text.RegularExpressions;

namespace Workflow.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateGroupPage : ContentPage
    {
        CreateGroupViewModel viewModel { get; set; }
        public CreateGroupPage(INavigation navigation)
        {
            InitializeComponent();
            BindingContext = viewModel = new CreateGroupViewModel(navigation);
            viewModel.ButtonText = "Создать группу";
            Submit.Clicked += CreateGroup;
        }

        public CreateGroupPage(INavigation navigation, GroupModel group)
        {
            InitializeComponent();
            BindingContext = viewModel = new CreateGroupViewModel(navigation);
            viewModel.Group = group;
            viewModel.ButtonText = "Редактировать группу";
            Submit.Clicked += EditGroup;
        }

        void CreateGroup(object sender, EventArgs args)
        {
            var group = new GroupModel(GroupName.Text, Description.Text);
            viewModel.CreateGroupCommand.Execute(group);
        }

        void EditGroup(object sender, EventArgs args)
        {
            viewModel.Group.Name = GroupName.Text;
            viewModel.Group.Description = Description.Text;
            viewModel.EditGroupCommand.Execute(null);
        }
    }
}