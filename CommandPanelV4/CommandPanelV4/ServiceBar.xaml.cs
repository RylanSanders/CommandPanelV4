﻿using CommandPanelV4.Config;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Media;
using CommandPanelV4.Util;

namespace CommandPanelV4
{
    /// <summary>
    /// Interaction logic for ServiceBar.xaml
    /// </summary>
    public partial class ServiceBar : UserControl
    {
        private const int TIMER_REFRESH_TIME = 1000;

        private System.Timers.Timer refreshTimer;

        public ServiceBar()
        {
            InitializeComponent();
            RefreshStatus();
            refreshTimer = new System.Timers.Timer(TIMER_REFRESH_TIME);
            
            refreshTimer.Elapsed += (sender, args)=>Dispatcher.Invoke(RefreshStatus);
            refreshTimer.Start();
        }

        private void RefreshStatus() 
        {
            if (String.IsNullOrEmpty(ServiceName)) return;
            ServiceXMLObject context=  DataContext as ServiceXMLObject;
            ServiceController sc = new ServiceController(ServiceName);
            if (SvcUtil.IsServiceInstalled(ServiceName))
            {
                if (sc.Status == ServiceControllerStatus.Running)
                {
                    StatusPackIcon.Visibility = Visibility.Visible;
                    StatusPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Check;
                    StatusPackIcon.Foreground = Brushes.Green;
                }
                else if (sc.Status == ServiceControllerStatus.Stopped)
                {
                    StatusPackIcon.Visibility = Visibility.Visible;
                    StatusPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Stop;
                    StatusPackIcon.Foreground = Brushes.Red;
                }
                else
                {
                    StatusPackIcon.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                StatusPackIcon.Visibility = Visibility.Visible;
                StatusPackIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.AlertOctagon;
                StatusPackIcon.Foreground = Brushes.Red;
            }
            
        }


        public string ServiceName
        {
            get { return (string)GetValue(ServiceNameProperty); }
            set { SetValue(ServiceNameProperty, value);  }
        }

        public static readonly DependencyProperty ServiceNameProperty =
        DependencyProperty.Register("ServiceName", typeof(string), typeof(ServiceBar));


        private void StartServiceButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceXMLObject context = DataContext as ServiceXMLObject;
            ServiceController sc = new ServiceController(ServiceName);
            if (SvcUtil.IsServiceInstalled(ServiceName) && sc.Status==ServiceControllerStatus.Stopped)
            {
                //Note: this requires run as admin 
                //TODO maybe prevent this using this https://stackoverflow.com/questions/1913429/servicecontroller-permissions ?
                //Could add an encrypted config for the user name/password for an admin account so I could run a certain thread while impersonating the admin specifically for any admin tasks
                try
                {
                    sc.Start();
                }
                catch(InvalidOperationException op)
                {
                    MessageBox.Show($"Cannot start service {ServiceName}. Restart this application as an admin!");
                }
                
            }
        }

        private void RestartService(ServiceController svcController)
        {
            try
            {
                if (svcController.Status == ServiceControllerStatus.Running)
                {
                    svcController.Stop();
                    svcController.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                svcController.Start();
            }
            catch (InvalidOperationException op)
            {
                MessageBox.Show($"Cannot stop service {ServiceName}. Restart this application as an admin!");
            }
        }

        private void StopServiceButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceXMLObject context = DataContext as ServiceXMLObject;
            ServiceController sc = new ServiceController(ServiceName);
            if (SvcUtil.IsServiceInstalled(ServiceName) && sc.Status == ServiceControllerStatus.Running)
            {
                try
                {
                    sc.Stop();
                }
                catch (InvalidOperationException op)
                {
                    MessageBox.Show($"Cannot stop service {ServiceName}. Restart this application as an admin!");
                }

            }
        }

        private void RestartServiceButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceXMLObject context = DataContext as ServiceXMLObject;
            ServiceController sc = new ServiceController(ServiceName);
            if (SvcUtil.IsServiceInstalled(ServiceName) )
            {
                Task.Run(() => RestartService(sc));

            }
        }

    }
}
