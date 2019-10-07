using MVVMCoffee.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using XamarinAppCsharp.Models;

namespace XamarinAppCsharp.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public MainPageViewModel()
        {
            NotesCollection = new ObservableCollection<NoteModel>();

            EraseNotesCommand = new Command(() =>
            {
                NoteText = string.Empty;
                NotesCollection.Clear();
            });
            SaveNoteCommand = new Command(() =>
            {
                var note = new NoteModel
                {
                    Text = NoteText
                };
                NotesCollection.Add(note);
                NoteText = string.Empty;
            });
        }

        public ObservableCollection<NoteModel> NotesCollection { get; set; }

        string noteText;

        public string NoteText
        {
            get => noteText;
            set
            {
                SetProperty(ref noteText, value, nameof(NoteText));
            }
        }


        public Command SaveNoteCommand { get; }
        public Command EraseNotesCommand { get; }
    }
}
