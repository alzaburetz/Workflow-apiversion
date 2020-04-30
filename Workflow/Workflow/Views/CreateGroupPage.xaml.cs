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
    public partial class CreateGroupPage : ContentPage
    {
        CreateGroupViewModel viewModel { get; set; }
        public CreateGroupPage(INavigation navigation)
        {
            InitializeComponent();
            BindingContext = viewModel = new CreateGroupViewModel(navigation);
        }

        void CreateGroup(object sender, EventArgs args)
        {
            var group = new GroupModel(GroupName.Text, Description.Text);
            viewModel.CreateGroupCommand.Execute(group);
        }
    }
}