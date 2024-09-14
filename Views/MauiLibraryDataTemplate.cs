using CommunityToolkit.Maui.Markup;

namespace HelloMaui;
using static GridRowsColumns;

public class MauiLibraryDataTemplate() : DataTemplate(() => CreateTemplate())
{
    private const int _imageRadius = 25;
    private const int _Padding = 8;
    private const int _titleHeight = 30;
    private const int _descriptionHeight = 25;


    static Grid CreateTemplate() => new Grid()
    {
        RowDefinitions = Rows.Define(
            (Row.Title, _titleHeight),
            (Row.Description, _descriptionHeight),
            (Row.BottomPadding, _Padding)),

        ColumnDefinitions = Columns.Define(
            (Column.Image, (_imageRadius * 2) + _Padding),
            (Column.Text, Star)),

        ColumnSpacing = 4,

        Children =
        {
            new Image()
                .Center()
                .Column(Column.Image)
                .Aspect(Aspect.AspectFit)
                .Margins(top:30)
                .Size(_imageRadius * 2)
                .Bind(Image.SourceProperty,
                    getter: (MauiLibrary model) => model.ImageUrl,
                    mode: BindingMode.OneWay),

            new Label()
                .Font(size: 22, bold: true)
                .Bind(Label.TextProperty,
                    getter: (MauiLibrary model) => model.Title,
                    mode: BindingMode.OneWay)
                .TextTop()
                .AppThemeColorBinding(Label.TextColorProperty, Color.FromArgb("#262626"), Color.FromArgb("#c9c9c9"))
                .TextStart()
                .Paddings(bottom:5)
                .Row(Row.Title)
                .Column(Column.Text),

            new Label()
                {
                    MaxLines = 2,
                    LineBreakMode = LineBreakMode.WordWrap
                }
                .FontSize(size: 16)
                .Bind(Label.TextProperty,
                    getter: (MauiLibrary model) => model.Description,
                    mode: BindingMode.OneWay)
                .TextTop()
                .AppThemeColorBinding(Label.TextColorProperty, Color.FromArgb("#262626"), Color.FromArgb("#c9c9c9"))
                .TextStart()
                .Paddings(bottom:5)
                .Row(Row.Description)
                .Column(Column.Text)

        }
    };

    enum Row { Title, Description, BottomPadding }
    enum Column { Image, Text }
}