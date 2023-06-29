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

namespace LoginSignUp.UserControls
{
    /// <summary>
    /// Interaction logic for Employee.xaml
    /// </summary>
    public partial class Employee : UserControl
    {
        public Employee()
        {
            InitializeComponent();
        }
        public delegate void DeleteUser(object sender, RoutedEventArgs e, string userName);
        public event DeleteUser _DeleteUser;
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            _DeleteUser(sender, e, EmployeeName.Text);
        }
    }
}
