using CommandPanelV4.Util;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CommandPanelV4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<ServiceObj> ServicesCollection = new ObservableCollection<ServiceObj>();
        public MainWindow()
        {
            InitializeComponent();
            ServicesCollection = new ObservableCollection<ServiceObj>();
            ServicesListView.ItemsSource = ServicesCollection;

            WindowUtil.ApplyDarkWindowStyle(this);

            ServicesCollection.Add(new ServiceObj() { Name = "XboxGipSvc" });
        }
    }
}