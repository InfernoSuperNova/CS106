using LoginSignUp.pages;
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
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : UserControl
    {
        public AddEmployee()
        {
            InitializeComponent();
            AddEmployeeField._ConfirmAddUser += ConfirmAddUser;
            AddEmployeeField._CancelAddUser += CancelAddUser;
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeField.Visibility = Visibility.Visible;
        }
        private void CancelAddUser(object sender, RoutedEventArgs e)
        {
            AddEmployeeField.Visibility = Visibility.Hidden;
        }

        public delegate void ConfirmAddUserMaster(object sender, RoutedEventArgs e, string userName);
        public event ConfirmAddUserMaster _ConfirmAddUserMaster;
        private void ConfirmAddUser(object sender, RoutedEventArgs e, string user)
        {
            AddEmployeeField.Visibility = Visibility.Hidden;
            _ConfirmAddUserMaster(sender, e, user);
        }
    }
}
