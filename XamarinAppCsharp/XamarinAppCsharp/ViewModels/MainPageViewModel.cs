using MVVMCoffee.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using XamarinAppCsharp.Models;
using XamarinAppCsharp.Views;

namespace XamarinAppCsharp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
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
                var timezone = await App.TimezoneManager.GetAsync();
                if(timezone != null) NoteText = timezone.datetime.ToString();
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


        public Command SaveNoteCommand { get; }
        public Command EraseNotesCommand { get; }
        public Command NoteSelectedCommand { get; }
        public Command GetCommand { get; }
    }
}
