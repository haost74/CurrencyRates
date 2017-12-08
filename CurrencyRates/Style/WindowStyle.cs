using System;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace CurrencyRates.Style
{
    public partial class WindowStyle
    {
        void IconMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount > 1)
                sender.FromTemplete(w => SystemCommands.CloseWindow(w));
        }

        void IconMouseUp(object sender, MouseButtonEventArgs e)
        {
            var element = sender as FrameworkElement;
            var point = element.PointToScreen(new Point(element.ActualWidth / 2, element.ActualHeight));
            sender.FromTemplete(w => SystemCommands.ShowSystemMenu(w, point));
        }

        void WindowLoaded(object sender, RoutedEventArgs e)
        {
            ((Window)sender).StateChanged += WindowStateChanged;
        }

        void WindowStateChanged(object sender, EventArgs e)
        {
            var w = ((Window)sender);
            var handle = w.GetHeader();
            var containerBorder = (Border)w.Template.FindName("PART_Container", w);

            if (w.WindowState == WindowState.Maximized)
            {
                // Make sure window doesn't overlap with the taskbar.
                var screen = System.Windows.Forms.Screen.FromHandle(handle);
                if (screen.Primary)
                {
                    //containerBorder.Padding = new Thickness(
                    //    SystemParameters.WorkArea.Left + 7,
                    //    SystemParameters.WorkArea.Top + 7,
                    //    (SystemParameters.PrimaryScreenWidth - SystemParameters.WorkArea.Right) + 7,
                    //    (SystemParameters.PrimaryScreenHeight - SystemParameters.WorkArea.Bottom) + 5);

                    try
                    {

                        containerBorder.Padding = new Thickness(
                            SystemParameters.WorkArea.Left + 7,
                            SystemParameters.WorkArea.Top + 7,
                           0,
                           0);
                    }
                    catch { }
                }
            }
            else
            {
                containerBorder.Padding = new Thickness(7, 7, 7, 5);
            }
        }

        void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            sender.FromTemplete(w => SystemCommands.CloseWindow(w));
        }

        void MinButtonClick(object sender, RoutedEventArgs e)
        {
            sender.FromTemplete(w => SystemCommands.MinimizeWindow(w));
        }

        void MaxButtonClick(object sender, RoutedEventArgs e)
        {
            sender.FromTemplete(w =>
            {
                if (w.WindowState == WindowState.Maximized) SystemCommands.RestoreWindow(w);
                else SystemCommands.MaximizeWindow(w);
            });
        }
    }
}
