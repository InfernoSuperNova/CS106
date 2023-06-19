using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoginSignUp.pages
{
    /// <summary>
    /// Interaction logic for ManageEmployees.xaml
    /// </summary>
    public partial class ManageEmployees : Page
    {
        public ManageEmployees()
        {
            InitializeComponent();

            
        }

        private void switchButton_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeBtn.Visibility = Visibility.Collapsed;
            AddEmployeeFieldBtn.Visibility = Visibility.Visible;
        }
    }
}
