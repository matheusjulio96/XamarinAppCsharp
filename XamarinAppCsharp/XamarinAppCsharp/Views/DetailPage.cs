using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using XamarinAppCsharp.ViewModels;

namespace XamarinAppCsharp.Views
{
    public class DetailPage : ContentPage
    {
        public DetailPage(DetailPageViewModel viewModel)
        {
            var stackLayout = new StackLayout();

            BindingContext = viewModel;
            Title = "Notes Detail";
            BackgroundColor = Color.PowderBlue;
            var textLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            textLabel.SetBinding(Label.TextProperty, nameof(DetailPageViewModel.NoteText));

            var exitButton = new Button
            {
                Text = "Back",
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(20),
                BackgroundColor = Color.Red,
                TextColor = Color.White,
                FontSize = 20
            };
            exitButton.SetBinding(Button.CommandProperty, nameof(DetailPageViewModel.BackButtonCommand));


            stackLayout.Children.Add(textLabel);
            stackLayout.Children.Add(exitButton);

            Content = stackLayout;

            Console.WriteLine("teste");
        }
    }
}
