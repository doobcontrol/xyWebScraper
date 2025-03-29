using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace configControlWpf
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand NewSearch = new RoutedUICommand
            (
                "NewSearch",
                "NewSearch",
                typeof(CustomCommands)
            );
        public static readonly RoutedUICommand DelSearch = new RoutedUICommand
            (
                "DelSearch",
                "DelSearch",
                typeof(CustomCommands)
            );
        public static readonly RoutedUICommand NewSearchLayer = new RoutedUICommand
            (
                "NewSearchLayer",
                "NewSearchLayer",
                typeof(CustomCommands)
            );
        public static readonly RoutedUICommand DelSearchLayer = new RoutedUICommand
            (
                "DelSearchLayer",
                "DelSearchLayer",
                typeof(CustomCommands)
            );
        public static readonly RoutedUICommand NewReplace = new RoutedUICommand
            (
                "NewReplace",
                "NewReplace",
                typeof(CustomCommands)
            );
        public static readonly RoutedUICommand DelReplace = new RoutedUICommand
            (
                "DelReplace",
                "DelReplace",
                typeof(CustomCommands)
            );
    }
}
