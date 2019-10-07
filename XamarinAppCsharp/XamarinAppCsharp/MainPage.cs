using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinAppCsharp.Models;
using XamarinAppCsharp.ViewModels;

namespace XamarinAppCsharp
{
    public class MainPage : ContentPage
    {
        Image xamagonImage;
        Editor noteEditor;
        Button saveButton, deleteButton;

        public MainPage()
        {
            BackgroundColor = Color.PowderBlue;

            Title = "Notes";

            BindingContext = new MainPageViewModel();

            xamagonImage = new Image
            {
                Source = "xamagon.png"
            };

            noteEditor = new Editor
            {
                Placeholder = "Enter note",
                BackgroundColor = Color.White,
                Margin = new Thickness(10)
            };
            noteEditor.SetBinding(Editor.TextProperty, nameof(MainPageViewModel.NoteText));

            saveButton = new Button
            {
                Text = "Save",
                TextColor = Color.White,
                BackgroundColor = Color.Green,
                Margin = new Thickness(10)
            };
            saveButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.SaveNoteCommand));

            deleteButton = new Button
            {
                Text = "Delete",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                Margin = new Thickness(10)
            };
            deleteButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.EraseNotesCommand));

            var collectionView = new CollectionView
            {
                ItemTemplate = new NotesTemplate(),
                SelectionMode = SelectionMode.Single
            };
            collectionView.SetBinding(CollectionView.ItemsSourceProperty, nameof(MainPageViewModel.Notes));
            collectionView.SetBinding(CollectionView.SelectedItemProperty, nameof(MainPageViewModel.SelectedNote));
            collectionView.SetBinding(CollectionView.SelectionChangedCommandProperty, nameof(MainPageViewModel.NoteSelectedCommand));

            var grid = new Grid
            {
                Margin = new Thickness(20, 40),

                ColumnDefinitions =
                {
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
                },
                RowDefinitions =
                {
                    new RowDefinition{ Height = new GridLength(1.0, GridUnitType.Star)},
                    new RowDefinition{ Height = new GridLength(2.5, GridUnitType.Star)},
                    new RowDefinition{ Height = new GridLength(1.0, GridUnitType.Star)},
                    new RowDefinition{ Height = new GridLength(2.0, GridUnitType.Star)}
                }
            };

            grid.Children.Add(xamagonImage, 0, 0);
            Grid.SetColumnSpan(xamagonImage, 2);

            grid.Children.Add(noteEditor, 0, 1);
            Grid.SetColumnSpan(noteEditor, 2);

            grid.Children.Add(saveButton, 0, 2);
            grid.Children.Add(deleteButton, 1, 2);

            grid.Children.Add(collectionView, 0, 3);
            Grid.SetColumnSpan(collectionView, 2);

            Content = grid;
        }
    }

    class NotesTemplate : DataTemplate
    {
        public NotesTemplate() : base(LoadTemplate)
        {
        }

        static StackLayout LoadTemplate()
        {
            var textLabel = new Label();
            textLabel.SetBinding(Label.TextProperty, nameof(NoteModel.Text));

            var frame = new Frame
            {
                VerticalOptions = LayoutOptions.Center,
                Content = textLabel
            };

            return new StackLayout
            {
                Children = { frame },
                Padding = new Thickness(10, 10)
            };
        }
    }
}
