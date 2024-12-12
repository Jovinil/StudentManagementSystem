using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using System;

namespace StudentManagementSystem.Views;

public partial class ClassPageView : UserControl
{
    public ClassPageView()
    {
        InitializeComponent();
        PopulateGrid(6); // Example: 6 items
    }

    private void PopulateGrid(int itemCount)
    {
        int columns = 3; // Number of columns
        int rows = (int)Math.Ceiling((double)itemCount / columns);

        // Create grid definitions dynamically
        for (int i = 0; i < columns; i++)
        {
            DynamicGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        }
        for (int i = 0; i < rows; i++)
        {
            DynamicGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        }

        // Add items to the grid
        for (int i = 0; i < itemCount; i++)
        {
            var card = CreateCard($"Grade {i + 1}");
            int row = i / columns;
            int column = i % columns;

            Grid.SetRow(card, row);
            Grid.SetColumn(card, column);
            DynamicGrid.Children.Add(card);
        }
    }

    private Border CreateCard(string title)
    {
        // Card container
        var cardBorder = new Border
        {
            BorderThickness = new Thickness(1),
            BorderBrush = Brushes.Gray,
           Background = new SolidColorBrush(Color.Parse("#1e1e1e")),
            CornerRadius = new CornerRadius(5),
            Margin = new Thickness(10),
            Padding = new Thickness(10)
        };

        // Card content
        var stackPanel = new StackPanel { Spacing = 5 };

        // Title
        stackPanel.Children.Add(new TextBlock
        {
            Text = title,
            FontWeight = FontWeight.Bold,
            FontSize = 16,
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
        });

        // Rows (Name and Score)
        for (int j = 0; j < 4; j++) // Example: 4 rows
        {
            var row = new StackPanel
            {
                Orientation = Avalonia.Layout.Orientation.Horizontal,
                Spacing = 10,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
            };

            row.Children.Add(new TextBlock { Text = "NAME SHIT", FontSize = 14 });
            row.Children.Add(new TextBlock { Text = "3.0", FontSize = 14 });

            stackPanel.Children.Add(row);
        }

        // View All button
        stackPanel.Children.Add(new Button
        {
            Content = "View All",
            HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Right,
     
        });

        cardBorder.Child = stackPanel;

        return cardBorder;
    }
}

