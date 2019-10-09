using MVVMCoffee.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinAppCsharp.Models;
using XamarinAppCsharp.Views;

namespace XamarinAppCsharp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        // if android
        static private string imagePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}/image123.png";
        //Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public MainPageViewModel()
        {
            Notes = new ObservableCollection<NoteModel>();

            EraseNotesCommand = new Command(() =>
            {
                NoteText = string.Empty;
                Notes.Clear();
            });
            SaveNoteCommand = new Command(() =>
            {
                Notes.Add(new NoteModel
                {
                    Text = NoteText
                });
                NoteText = string.Empty;
            },
            () => !string.IsNullOrEmpty(NoteText));

            NoteSelectedCommand = new Command(async () =>
            {
                if (SelectedNote is null)
                    return;

                var detailViewModel = new DetailPageViewModel
                {
                    NoteText = SelectedNote.Text
                };
                await Application.Current.MainPage.Navigation.PushAsync(new DetailPage(detailViewModel));
                SelectedNote = null;
            });

            GetCommand = new Command(async () =>
            {
                //var timezone = await App.TimezoneManager.GetAsync();
                //if(timezone != null) NoteText = timezone.datetime.ToString();

                string str = await App.TimezoneManager.restService.HttpGetString("http://192.168.1.149:45458/api/values");
                NoteText = str;
            });

            SaveImageCommand = new Command(async () =>
            {
                var image = await App.TimezoneManager.restService.HttpGetByteArray("https://www.google.com/images/branding/googlelogo/1x/googlelogo_color_272x92dp.png");

                
                string tempFileName = imagePath;
                File.WriteAllBytes(tempFileName, image);

                var imageAsBytes = File.ReadAllBytes(tempFileName);

                ImageSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));
            });

            GetPrefCommand = new Command(() =>
            {
                var anUserPref = Preferences.Get("anUserPref", 0);
                NoteText = anUserPref.ToString();
            });

            SavePrefCommand = new Command(() =>
            {
                int anUserPref = 0;
                if (int.TryParse(NoteText, out anUserPref))
                {
                    Preferences.Set("anUserPref", anUserPref);
                }
            });
        }

        public ObservableCollection<NoteModel> Notes { get; set; }

        string noteText;

        public string NoteText
        {
            get => noteText;
            set
            {
                SetProperty(ref noteText, value, nameof(NoteText));
                SaveNoteCommand.ChangeCanExecute();
            }
        }
        NoteModel selectedNote;
        public NoteModel SelectedNote { get => selectedNote; 
            set {
                SetProperty(ref selectedNote, value, nameof(SelectedNote));
            } 
        }

        ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(File.ReadAllBytes(imagePath)));
        public ImageSource ImageSource
        {
            get => imageSource;
            set
            {
                SetProperty(ref imageSource, value, nameof(ImageSource));
            }
        }


        public Command SaveNoteCommand { get; }
        public Command EraseNotesCommand { get; }
        public Command NoteSelectedCommand { get; }
        public Command GetCommand { get; }
        public Command SaveImageCommand { get; }
        public Command GetPrefCommand { get; }
        public Command SavePrefCommand { get; }
    }
}
