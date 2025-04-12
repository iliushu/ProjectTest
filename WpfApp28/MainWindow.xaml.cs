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

namespace WpfApp28;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly MainWindowModel _vm = new MainWindowModel();
    public MainWindow()
    {
        InitializeComponent();
        DataContext = _vm;
    }
    private void ChangeTheme_Click(object sender, RoutedEventArgs e)
    {
        _vm.UpdateConfig("ThemeColor", "#FFFF5000"); // 修改为红色
        _vm.UpdateConfig("Name", "123"); // 修改为红色
    }
}