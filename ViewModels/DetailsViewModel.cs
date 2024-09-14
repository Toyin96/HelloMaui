using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace HelloMaui.ViewModels;

public class DetailsViewModel : BaseViewModel, IQueryAttributable
{
    public const string DetailsPageKey = nameof(DetailsPageKey);

    private ICommand _navigateCommand;
    private ImageSource? _imageSource;
    private string _labelDescription;
    private string _labelTitle;

    public ImageSource? ImageSource
    {
        get => _imageSource;
        set => SetField(ref _imageSource, value);
    }
    public string LabelDescription
    {
        get => _labelDescription;   
        set => SetField(ref _labelDescription, value);
    }

    public string LabelTitle
    {
        get => _labelTitle;
        set => SetField(ref _labelTitle, value);
    }
    public ICommand NavigateCommand
    {
        get => _navigateCommand;
    }

    public DetailsViewModel()
    {
        _navigateCommand = new AsyncRelayCommand(async () => await HandleBackButtonClicked());
    }
    
    private async Task HandleBackButtonClicked()
    {
        await Shell.Current.GoToAsync("..", true);
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var model = (MauiLibrary)query[DetailsPageKey];
        
        LabelTitle = model.Title; // set the content page title
        
        ImageSource = model.ImageUrl; 
        LabelTitle = model.Title; 
        LabelDescription = model.Description;
    }
}