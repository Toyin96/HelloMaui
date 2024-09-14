using CommunityToolkit.Maui.Markup;
using HelloMaui.ViewModels;

namespace HelloMaui;

public class DetailsPage : BaseContentPage<DetailsViewModel>
{
    public DetailsPage(DetailsViewModel detailsViewModel) : base(detailsViewModel)
    {
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior()
        {
            TextOverride = "mumu",
            //IconOverride = "appicon.svg",
            //want to test somethin
        });

        this.Bind(TitleProperty,
            getter: (DetailsViewModel vm) => vm.LabelTitle);

        BackgroundColor = Color.FromHex("#FFFFFF");
        Padding = new Thickness(10);

        Content = new VerticalStackLayout()
        {
            Spacing = 10,

            Children =
            {
                new Image()
                    {
                        BindingContext = detailsViewModel
                    }
                    .Size(250)
                    .Bind(Image.SourceProperty,
                        getter: (DetailsViewModel vm) => vm.ImageSource),

                new Label()
                    .Font(size: 32, bold: true)
                    .AppThemeColorBinding(Label.TextColorProperty, Colors.Black, Colors.Black)
                    .Center()
                    .TextCenter()
                    .Bind(Label.TextProperty,
                        getter: (DetailsViewModel vm) => vm.LabelTitle,
                        setter: (DetailsViewModel vm, string title) => vm.LabelTitle = title),

                new Label()
                    .Center()
                    .AppThemeColorBinding(Label.TextColorProperty, Colors.Black, Colors.Black)
                    .TextCenter()
                    .Bind(Label.TextProperty,
                        getter: (DetailsViewModel vm) => vm.LabelDescription,
                        setter: (DetailsViewModel vm, string description) => vm.LabelDescription = description),

                new Button()
                    .Text("Back")
                    .Bind(Button.CommandProperty,
                        getter: (DetailsViewModel viewModel) => viewModel.NavigateCommand)
            },

            HorizontalOptions = LayoutOptions.Center,
        }.Center();
    }
}