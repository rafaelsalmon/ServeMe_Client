using System;
using SirvaMe.CustomControls;
using SirvaMe.Interfaces;
using Xamarin.Forms;

namespace SirvaMe.Menu
{
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
            Icon = "ic_action_action_reorder.png";
            Title = "Menu";
            BackgroundColor = Color.White;

            var faceBookId = App.Current.UserFacebookID;

            var avatarFacebook = !string.IsNullOrEmpty(faceBookId)
                                    ? $"https://graph.facebook.com/{faceBookId}/picture?type=large"
                                    : "icon_profile2.png";

            Menu = new MenuListView { SelectedItem = 0 };

            var grid = new Grid
            {
                Padding = new Thickness(20, 5, Device.OnPlatform(10, 0, 0), 5),
                BackgroundColor = (Color)Application.Current.Resources["MenuColor"],
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions =
                {
                    new RowDefinition { Height = 60 }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = 60 },
                    new ColumnDefinition { Width = 100 },
                    new ColumnDefinition { Width = 100 }
                }
            };

            grid.Children.Add(new CircleImage
            {
                Source = avatarFacebook,
                Aspect = Aspect.AspectFill,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                HeightRequest = 60,
                WidthRequest = 60
            }, 0, 0);

            grid.Children.Add(new Label
            {
                Text = RetornaNomeAbreviado(),
                FontSize = 14,
                TextColor = Color.White,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center
            }, 1, 0);

            var perfilButton = new Button
            {
                Text = "VER PERFIL",
                FontSize = 12,
                TextColor = (Color)Application.Current.Resources["MenuFontColor"],
                BackgroundColor = (Color)Application.Current.Resources["MenuColor"],
                HorizontalOptions = LayoutOptions.Start
            };
            perfilButton.Clicked += VerPerfilOnButtonClicked;

            grid.Children.Add(perfilButton, 2, 0);

            var layout = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(19, 0, 0), 0, 0),
                Spacing = 0,
                BackgroundColor = (Color)Application.Current.Resources["BarBackgroundColor"],
                VerticalOptions = LayoutOptions.StartAndExpand
            };

            var logoMenuIos = new StackLayout
            {
                Padding = new Thickness(20, 10, 5, 10),
                BackgroundColor = (Color)Application.Current.Resources["BarBackgroundColor"],
                Spacing = 0,
                Children =
                {
                    new Image
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        Aspect = Aspect.AspectFill,
                        Source = "logo_header.png"
                    }
                }
            };
            if (Device.OS == TargetPlatform.iOS)
                layout.Children.Add(logoMenuIos);

            var config = DependencyService.Get<IConfig>();

            var versao = new StackLayout
            {
                Padding = new Thickness(20, 0, 0, 0),
                BackgroundColor = Color.White,
                Children =
                {
                    new Label
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        TextColor = Color.Gray,
                        Text = $"© {DateTime.Now.Year} Sirva-Me - Versão {config.GetBuildNumber}"
                    }
                }
            };

            layout.Children.Add(grid);
            layout.Children.Add(Menu);
            layout.Children.Add(versao);

            Content = layout;
        }

        private static string RetornaNomeAbreviado()
        {
            try
            {
                var nomes = App.Current.UserName.ToUpper().Split();
                return $"{nomes[0]} {nomes[1].Substring(0, 1)}.";// {App.Current.UserID}"; //TDO Remover ID após os testes
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private static void VerPerfilOnButtonClicked(object sender, EventArgs e)
        {
            App.Current.ShowPerfilPage();
        }
    }
}
