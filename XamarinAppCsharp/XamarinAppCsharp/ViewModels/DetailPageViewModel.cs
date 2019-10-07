using MVVMCoffee.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinAppCsharp.ViewModels
{
    public class DetailPageViewModel : BaseViewModel
    {
        public DetailPageViewModel()
        {
            BackButtonCommand = new Command(async () => await Application.Current.MainPage.Navigation.PopAsync());
        }

        string noteText;
        public string NoteText { get => noteText;
            set {
                SetProperty(ref noteText, value, nameof(NoteText));
            }
        }

        public Command BackButtonCommand { get; set; }
    }
}
