using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Workflow.Models;
using Workflow.Utils;

using Xamarin.Forms;

namespace Workflow.ViewModels
{
    public class CreateGroupViewModel:BaseViewModel
    {
        public Command CreateGroupCommand { get; set; }
        readonly INavigation Navigation;
        public CreateGroupViewModel(INavigation nav)
        {
            this.Navigation = nav;
            CreateGroupCommand = new Command<GroupModel>((group) =>
            {
                Task.Run(async () =>
                {
                var response = await HttpService.PostRequest<ResponseModel<GroupModel>, GroupModel>("groups/create", group, true);
                if (response.Code == 200)
                {
                    Device.BeginInvokeOnMainThread(async () => await Navigation.PopAsync());
                 }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () => await Application.Current.MainPage.DisplayAlert("Ошибка", ListStringifyer.StringifyList(response.Errors), "OK"));
                    }
                });
            });
        }
    }
}
