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
        Button saveButton, deleteButton, getButton, imageButton, savePrefButton, getPrefButton;

        public MainPage()
        {
            BackgroundColor = Color.PowderBlue;

            int columnCount = 2;

            Title = "Notes";

            BindingContext = new MainPageViewModel();

            xamagonImage = new Image
            {
                Source = "xamagon.png"
            };
            xamagonImage.SetBinding(Image.SourceProperty, nameof(MainPageViewModel.ImageSource));

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

            getButton = new Button
            {
                Text = "Get",
                TextColor = Color.White,
                BackgroundColor = Color.LightBlue,
                Margin = new Thickness(10)
            };
            getButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.GetCommand));
            imageButton = new Button
            {
                Text = "Image",
                TextColor = Color.White,
                BackgroundColor = Color.BlueViolet,
                Margin = new Thickness(10)
            };
            imageButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.SaveImageCommand));

            savePrefButton = new Button
            {
                Text = "savePref",
                TextColor = Color.White,
                BackgroundColor = Color.BlueViolet,
                Margin = new Thickness(10)
            };
            savePrefButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.SavePrefCommand));
            getPrefButton = new Button
            {
                Text = "getPref",
                TextColor = Color.White,
                BackgroundColor = Color.BlueViolet,
                Margin = new Thickness(10)
            };
            getPrefButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.GetPrefCommand));

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
                    new RowDefinition{ Height = new GridLength(2.0, GridUnitType.Star)},
                    new RowDefinition{ Height = new GridLength(1.0, GridUnitType.Star)},
                    new RowDefinition{ Height = new GridLength(2.0, GridUnitType.Star)},
                    new RowDefinition{ Height = new GridLength(1.0, GridUnitType.Star)}
                }
            };

            grid.Children.Add(xamagonImage, 0, 0);
            Grid.SetColumnSpan(xamagonImage, columnCount);

            grid.Children.Add(noteEditor, 0, 1);
            Grid.SetColumnSpan(noteEditor, columnCount);

            grid.Children.Add(saveButton, 0, 2);
            grid.Children.Add(deleteButton, 1, 2);
            grid.Children.Add(getButton, 2, 2);

            grid.Children.Add(collectionView, 0, 3);
            Grid.SetColumnSpan(collectionView, columnCount);

            grid.Children.Add(imageButton, 0, 4);
            grid.Children.Add(savePrefButton, 1, 4);
            grid.Children.Add(getPrefButton, 2, 4);

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
