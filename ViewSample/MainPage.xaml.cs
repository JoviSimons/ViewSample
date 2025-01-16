using System.Diagnostics;

namespace ViewSample
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            SetupLayout();
        }

        public void SetupLayout(bool isResize = false)
        {
            // Determine layout
            var grid = new Grid
            {
                HorizontalOptions = LayoutOptions.Fill,
                Padding = 0,
                IsClippedToBounds = false,
                // Was StackLayout (Vertical) in Xamarin with StartAndExpand as VerticalOptions
                // Making this VerticalOptions Start thus makes sense
                VerticalOptions = LayoutOptions.Start
            };

            grid.BackgroundColor = Colors.Green;
            var rowCounter = 0;

            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            grid.AddToGrid(createNewSection(isResize), row: rowCounter);
            

            try
            {

                // Add empty space to make sure the save button does not cover the content
                grid.RowDefinitions.Add(new RowDefinition());
                grid.AddToGrid(new BoxView
                {
                    HeightRequest = 100,
                    Color = Colors.Transparent
                }, row: rowCounter++);

                var frame = new Border
                {
                    Stroke = Colors.Green,
                    Content = grid
                };

                frame.HorizontalOptions = LayoutOptions.Fill;
                frame.MaximumWidthRequest = 800 > 0
                    ? 800
                    : 5000;

                element.Dispatcher.Dispatch(() =>
                {
                    element.BackgroundColor = Colors.White;

                    element.Content = new ScrollView
                    {
                        Content = frame,
                        Padding = 10,
                        HorizontalOptions = LayoutOptions.Fill
                    };
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to initialize the form editor: " + ex.Message);
                // TODO
            }

        }

        private View createNewSection(bool isResize)
        {
            var grid = new Grid
            {
                Padding = 10,
               // BackgroundColor = ,
                ColumnSpacing = 10,
                IsClippedToBounds = false
            };


            var columnCounter = 0;

            grid.ColumnDefinitions.Add(new ColumnDefinition
            {
                Width = new GridLength(1, GridUnitType.Star)
            });

            // Add stacklayout
            var columnStack = createNewColumn(isResize, 1);
            grid.AddToGrid(columnStack, columnCounter);



            var frame = new Border
            {
                Stroke = Colors.Red,
                Content = grid
            };

            frame.VerticalOptions = LayoutOptions.Start;
            // the setted frame request (difference between the 2 pictures)
            //frame.HeightRequest = 600;
            frame.Margin = new Thickness(10, 10, 10,
                10);

            return frame;
        }

        private View createNewColumn(bool isResize, int totalColumnWeight)
        {
            var stack = new Grid
            {
                Padding = 5,
                //BackgroundColor = column.ColumnBackgroundColor,
                RowSpacing = 5,
                IsClippedToBounds = false
            };

            var rowCounter = 0;



            foreach (var field in Enumerable.Range(0, 10))
            {

                try
                {
                    var button = new Button
                    {
                        HeightRequest = 50,
                        Text = "Button " + rowCounter.ToString(),
                    };

                    stack.AddRowDefinition(new RowDefinition(GridLength.Auto));
                    stack.AddToGrid(button, row: rowCounter++);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(string.Format("Failed to get the view for field {0}", ex));
                    throw;
                }
            }

            var frame = new Border
            {
                Stroke = Colors.Purple,
                Content = stack
            };

            frame.VerticalOptions = LayoutOptions.Start;

            return frame;
        }
    }

}
