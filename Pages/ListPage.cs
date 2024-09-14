using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Views;
using HelloMaui.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace HelloMaui;

public class ListPage : BaseContentPage<ListViewModel>
{
    public ListPage(ListViewModel viewModel) : base(viewModel)
    {
        this.AppThemeBinding(BackgroundColorProperty, Colors.LightBlue, Color.FromArgb("#3b4a4f"));
        Padding = new Thickness(0);

        Content = new RefreshView()
        {
            Content = new CollectionView()
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,

                Header = new SearchBar()
                {
                    Behaviors =
                        {
                            new UserStoppedTypingBehavior()
                            {
                                BindingContext = viewModel,
                                StoppedTypingTimeThreshold = 1000,
                                ShouldDismissKeyboardAutomatically = true,
                            }.Bind(UserStoppedTypingBehavior.CommandProperty,
                                getter: (ListViewModel vm) => vm.SearchItemsCommand)
                        },
                }
                    .Placeholder("Search for your preferred libraries")
                    .BackgroundColor(Colors.LightBlue)
                    .Bind(SearchBar.TextProperty,
                        getter: (ListViewModel vm) => viewModel.SearchBarText,
                        setter: (ListViewModel vm, string text) => viewModel.SearchBarText = text)
                    .TapGesture(async () =>
                    {
                        await Toast.Make("Baba you don try!").Show();
                    }, 2),
                ItemTemplate = new MauiLibraryDataTemplate(),
                SelectionMode = SelectionMode.Single,
                Footer = _footer
            }.Bind(CollectionView.ItemsSourceProperty,
                    getter: (ListViewModel vm) => vm.MauiLibraries)
                .Bind(CollectionView.SelectionChangedCommandProperty,
                getter: (ListViewModel vm) => vm.ItemSelectedCommand)
            .Bind(CollectionView.SelectedItemProperty,
                getter: (ListViewModel vm) => vm.SelectedLibrary,
                setter: (ListViewModel vm, MauiLibrary? mauiLibrary) => vm.SelectedLibrary = mauiLibrary)
        }.Bind(RefreshView.CommandProperty,
                getter: (ListViewModel vm) => vm.RefreshCommand)
            .Bind(RefreshView.IsRefreshingProperty,
            getter: (ListViewModel vm) => vm.IsRefreshing);
        //.Margins(top:24);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        this.ShowPopup(new WelcomePopup());
    }

    private readonly VerticalStackLayout _footer = new()
    {
        Children =
        {
            new Label()
                .Font(size: 12, bold: true)
                .Text("Brought to you by this.toyin @ MAGNA")
                .AppThemeColorBinding(Label.TextColorProperty, Color.FromArgb("#262626"), Color.FromArgb("#c9c9c9"))
                .TextCenter()
                .Center()
                //.Paddings(bottom:5)
        }
    };
}

public class WelcomePopup : Popup
{
    public WelcomePopup()
    {
        Content = new Label()
            .Text("Welcome to .NET MAUI applications.")
            .TextCenter()
            .Height(300.0)
            .Width(250.0)
            .TextColor(Colors.Coral)
            .Center()
            .Font(size: 24, bold: true);
    }
}