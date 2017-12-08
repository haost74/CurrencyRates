using System.Text;
using System.Windows.Input;

namespace CurrencyRates.ViewModel.Commands
{
    public class WindowsCommand
    {
        public static RoutedCommand Start { get; set; }

        static WindowsCommand()
        {
            Start = new RoutedCommand("Start", typeof(Encoder));
        }
    }
}
