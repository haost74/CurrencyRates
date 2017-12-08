using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace CurrencyRates.Style
{
    internal static class WindowStyleExtensions
    {
        /// <summary>
        /// Возвращает ссылку на родитель шаблона данного элемента.Данное свойство не имеет
        /// смысла, если элемент не был создан с помощью шаблона.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="action"></param>
        public static void FromTemplete(this object element, Action<Window> action)
        {
            Window win = ((FrameworkElement)element).TemplatedParent as Window;
            if (win != null) action(win);
        }

        /// <summary>
        /// Возвращает дескриптор окна, a Windows Presentation Foundation (WPF) окно, которое
        /// используется для создания данного System.Windows.Interop.WindowInteropHelper.
        /// </summary>
        /// <param name="win">окно</param>
        /// <returns></returns>
        public static IntPtr GetHeader(this Window win)
        {
            WindowInteropHelper help = new WindowInteropHelper(win);
            return help.Handle;
        }
    }
}
