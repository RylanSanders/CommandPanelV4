using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CommandPanelV4.Util
{
    public class WindowUtil
    {
        public static void ApplyDarkWindowStyle(Window window)
        {
            //Gets the style which includes the control template
            ResourceDictionary res = (ResourceDictionary)Application.LoadComponent(new Uri("../ResourceDictionary.xaml", UriKind.Relative));
            window.Style = (Style)res["DarkWindow"];
            window.DataContext = window;
            //I guess we need to apply the template before trying to find the buttons - They aren't part of the visual tree yet
            window.ApplyTemplate();
            var closeButton = window.Template.FindName("CloseButton", window) as Button;
            if (closeButton != null)
            {
                closeButton.Click += (sender, args) => window.Close();
            }
            var MinButton = window.Template.FindName("MinButton", window) as Button;
            if (MinButton != null)
            {
                MinButton.Click += (sender, args) => window.WindowState = WindowState.Minimized;
            }
            var MaxButton = window.Template.FindName("MaxButton", window) as Button;
            if (MaxButton != null)
            {
                MaxButton.Click += (sender, args) =>
                {
                    if (window.WindowState == WindowState.Maximized)
                    {
                        window.WindowState = WindowState.Normal;
                    }
                    else
                    {
                        window.WindowState = WindowState.Maximized;
                    }
                };
            }
            var TitleBar = window.Template.FindName("MaxButton", window) as DockPanel;
            if (TitleBar != null)
            {
                TitleBar.MouseDown += (sender, args) =>
                {
                    if (args.ChangedButton == MouseButton.Left)
                        if (args.ClickCount == 2)
                        {
                            if (window.WindowState == WindowState.Maximized)
                            {
                                window.WindowState = WindowState.Normal;
                            }
                            else
                            {
                                window.WindowState = WindowState.Maximized;
                            }
                        }
                        else
                        {
                            Application.Current.MainWindow.DragMove();
                        }
                };
            }
        }
    }
}
