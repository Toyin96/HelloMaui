using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace HelloMaui.ViewModels;

public class ListViewModel : BaseViewModel
{
    private readonly ICommand _itemSelectedCommand;
    private readonly ICommand _refreshCommand;
    private readonly ICommand _searchItemsCommand;
    private MauiLibrary? _selectedLibrary;
    private bool _isRefreshing;
    private string _searchBarText;
    private bool _isSearchBarEnabled;
    private ObservableCollection<MauiLibrary> _mauiLibraries = new ( GetDataSource());

    public bool IsRefreshing
    {
        get => _isRefreshing;
        set => SetField(ref _isRefreshing, value);
    }

    public ICommand SearchItemsCommand
    {
        get => _searchItemsCommand;
    }
    public bool IsSearchBarEnabled
    {
        get => _isSearchBarEnabled;
        set => SetField(ref _isSearchBarEnabled, value);
    }

    public string SearchBarText
    {
        get => _searchBarText;
        set => SetField(ref _searchBarText, value);
    }
    public ICommand RefreshCommand
    {
        get => _refreshCommand;
    }

    // NB: observableCollection is preferred because the amount the collection changes, it automatically
    // updates the collection view unlike list or IEnumerable where its manual
    public ObservableCollection<MauiLibrary> MauiLibraries
    {
        get => _mauiLibraries;
        set => SetField(ref _mauiLibraries, value);
    }

    public ICommand ItemSelectedCommand
    {
        get => _itemSelectedCommand;
    }

    public MauiLibrary? SelectedLibrary
    {
        get => _selectedLibrary;
        set => SetField(ref _selectedLibrary, value);
    }
    
    public ListViewModel()
    {
        _itemSelectedCommand = new AsyncRelayCommand(async () => await HandlerSelectionChanged());
        _refreshCommand = new AsyncRelayCommand(async () => await HandleRefreshing());
        _searchItemsCommand = new RelayCommand(() => SearchItemsForUser());
    }
    
    private async Task HandlerSelectionChanged()
    {
        if (_selectedLibrary != null)
        {
            await Shell.Current.GoToAsync(AppRoutes.FetchRoute<DetailsPage>(), new Dictionary<string, object>()
            {
                [DetailsViewModel.DetailsPageKey] = _selectedLibrary
            });
        }
        
        _selectedLibrary = null;
    }
    
    private async Task HandleRefreshing()
    {
        // disable search bar
         IsSearchBarEnabled = false;

        await Task.Delay(TimeSpan.FromSeconds(2));
        
        MauiLibraries.Add(
            new MauiLibrary()
            {
                Title = "Sharpnado.Tabs",  
                Description = "Pure Maui and Xamarin.form tabs, scrollable tabs, bottom tabs, badge",
                ImageUrl = "https://api.nuget.org/v3-flatcontainer/sharpnado.tabs/2.2.0/icon"
            });

        IsRefreshing = false;
        IsSearchBarEnabled = true; // enable it back after refreshing the list
    }

        static List<MauiLibrary> GetDataSource() =>
    [
        new MauiLibrary
        {
            Title = "CommunityToolkit.Maui",
            Description =
                "A collection of common helpers, converters, and UI controls for building .NET MAUI applications. It includes features like StatusBar, TabView, Expander, and StateContainer.",
            ImageUrl = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.mvvm/8.2.0/icon"
        },

        new MauiLibrary
        {
            Title = "Syncfusion .NET MAUI Controls",
            Description =
                "A suite of UI controls for building modern, cross-platform applications using .NET MAUI. It includes controls like charts, data grids, gauges, calendars, and more.",
            ImageUrl = "https://api.nuget.org/v3-flatcontainer/microsoft.maui.controls/8.0.3/icon"
        },

        new MauiLibrary
        {
            Title = "DevExpress .NET MAUI Controls",
            Description =
                "A set of high-performance controls optimized for .NET MAUI applications. It includes charts, data editors, gauges, and more, with a focus on enterprise-level applications.",
            ImageUrl = "https://api.nuget.org/v3-flatcontainer/esri.arcgisruntime.maui/100.14.1-preview3/icon"
        },

        new MauiLibrary
        {
            Title = "Telerik UI for .NET MAUI",
            Description =
                "A comprehensive UI component suite for .NET MAUI development that includes charts, data grids, gauges, calendars, and more.",
            ImageUrl = "https://api.nuget.org/v3-flatcontainer/syncfusion.maui.core/21.2.10/icon",
        },

        new MauiLibrary
        {
            Title = "Shiny.NET MAUI",
            Description =
                "A library that provides cross-platform APIs for accessing native device services, such as geolocation, notifications, sensors, and more.",
            ImageUrl = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.maui/5.2.0/icon"
        },

        new MauiLibrary
        {
            Title = "Refractored.Maui.FFImageLoading",
            Description =
                "A library for loading and caching images in .NET MAUI applications. It supports loading images from remote URLs, caching, transformations, and more.",
            ImageUrl = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.maui.markup/3.2.0/icon"
        },

        new MauiLibrary
        {
            Title = "Rg.Plugins.Popup for .NET MAUI",
            Description =
                "A popular plugin for creating customizable pop-ups and modals in .NET MAUI applications. It supports animations, complex layouts, and user interaction.",
            ImageUrl = "https://api.nuget.org/v3-flatcontainer/sentry.maui/3.33.1/icon"
        },

        new MauiLibrary
        {
            Title = "ReactiveUI for .NET MAUI",
            Description =
                "A framework that enables building reactive applications using the MVVM pattern in .NET MAUI. It provides reactive bindings, observables, and other features for building highly interactive applications.",
            ImageUrl = "https://api.nuget.org/v3-flatcontainer/microsoft.maui.controls/8.0.3/icon",
        },

        new MauiLibrary
        {
            Title = "Prism.Maui",
            Description =
                "A library that provides an MVVM framework for .NET MAUI applications. It helps with application architecture, navigation, dependency injection, and more.",
            ImageUrl =  "https://api.nuget.org/v3-flatcontainer/esri.arcgisruntime.maui/100.14.1-preview3/icon"
        },

        new MauiLibrary
        {
            Title = "Plugin.Permissions",
            Description =
                "A cross-platform plugin for managing and requesting permissions in .NET MAUI applications. It simplifies the process of handling permissions like location, camera, storage, etc.",
            ImageUrl =  "https://api.nuget.org/v3-flatcontainer/syncfusion.maui.core/21.2.10/icon"
        }
    ];
        
    private void SearchItemsForUser()
    {
        // Use a temporary list to hold filtered items before adding them to the ObservableCollection
        var filteredItems = new List<MauiLibrary>();
        var tempList = GetDataSource();

        if (tempList == null)
            return; // Safeguard against null data source

        // Determine which items to add based on the search text
        if (string.IsNullOrWhiteSpace(SearchBarText))
        {
            filteredItems.AddRange(tempList);
        }
        else
        {
            // Filter items based on the search text, case-insensitive
            filteredItems.AddRange(tempList.Where(x => x.Title != null &&
                                                       x.Title.Contains(SearchBarText, StringComparison.OrdinalIgnoreCase)));
        }

        // Ensure updates are performed on the main thread
        Application.Current?.Dispatcher.Dispatch(() =>
        {
            MauiLibraries.Clear();
            foreach (var item in filteredItems)
            {
                MauiLibraries.Add(item);
            }
        });
    }
}