
namespace ViewSample
{
    public static class Extension
    {
        public static void AddToGrid<T>(
            this Grid grid,
            T view,
            int column = 0,
            int row = 0,
            int columnSpan = 1,
            int rowSpan = 1
        ) where T : BindableObject, IView
        {
            Grid.SetColumn(view, column);
            Grid.SetRow(view, row);
            Grid.SetColumnSpan(view, columnSpan);
            Grid.SetRowSpan(view, rowSpan);

            grid.Children.Add(view);
        }
    }
}
