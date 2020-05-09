using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;

using Workflow.ViewModels.PopupViewModels;

namespace Workflow.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNotePopup : PopupPage
    {
        AddNotePopupViewModel viewModel { get; set; }
        public AddNotePopup(AddNotePopupViewModel vm)
        {
            InitializeComponent();
            BindingContext = viewModel = vm;
        }

        protected override bool OnBackgroundClicked()
        {
            return false;

        }
    }
}