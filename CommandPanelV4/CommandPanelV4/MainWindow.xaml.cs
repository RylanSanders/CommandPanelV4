using CommandPanelV4.Config;
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
        ObservableCollection<ServiceXMLObject> ServicesCollection = new ObservableCollection<ServiceXMLObject>();
        public MainWindow()
        {
            InitializeComponent();
            ServicesCollection = new ObservableCollection<ServiceXMLObject>();
            ServicesListView.ItemsSource = ServicesCollection;

            WindowUtil.ApplyDarkWindowStyle(this);

            ConfigUtil.GetConfig().Services.ForEach(svc => ServicesCollection.Add(svc));
        }
    }
}