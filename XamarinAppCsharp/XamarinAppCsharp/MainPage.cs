using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinAppCsharp
{
    public class MainPage : ContentPage
    {
        Image xamagonImage;
        Editor noteEditor;
        Label textLabel;
        Button saveButton, deleteButton;

        public MainPage()
        {
            BackgroundColor = Color.PowderBlue;

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

            saveButton = new Button
            {
                Text = "Save",
                TextColor = Color.White,
                BackgroundColor = Color.Green,
                Margin = new Thickness(10)
            };
            saveButton.Clicked += SaveButton_Clicked;

            deleteButton = new Button
            {
                Text = "Delete",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                Margin = new Thickness(10)
            };
            deleteButton.Clicked += DeleteButton_Clicked;

            textLabel = new Label
            {
                FontSize = 20,
                Margin = new Thickness(10)
            };

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

            grid.Children.Add(textLabel, 0, 3);
            Grid.SetColumnSpan(textLabel, 2);

            Content = grid;
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            textLabel.Text = "";
            noteEditor.Text = "";
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            textLabel.Text = noteEditor.Text;
        }
    }
}
