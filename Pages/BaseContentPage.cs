using HelloMaui.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;

namespace HelloMaui;

public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel
{
    protected BaseContentPage(T viewModel)
    {
        BindingContext = viewModel; // every subtype must pass the view model

        On<iOS>().SetUseSafeArea(true);
    }
}