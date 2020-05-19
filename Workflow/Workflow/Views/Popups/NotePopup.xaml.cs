using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Rg.Plugins.Popup.Pages;

namespace Workflow.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotePopup : PopupPage
    {
        public NotePopup(string date, string note)
        {
            InitializeComponent();
            Date.Text = date;
            Note.Text = note;
        }
    }
}