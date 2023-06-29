using LoginSignUp.classes;
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
    public partial class ManagedEmployee : UserControl
    {
        public ManagedEmployee()
        {
            InitializeComponent();
        }

        private void ShowPasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            bool isOpen = PasswordField.Visibility == Visibility.Visible;
            if (isOpen)
            {
                PasswordField.Visibility = Visibility.Collapsed;
                ShowPasswordText.Text = "Show Password";
            }
            else
            {
                PasswordField.Visibility = Visibility.Visible;
                ShowPasswordText.Text = "Hide Password";
            }
        }

        private void ChangeUserTypeBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserTypeBtn.Visibility = Visibility.Collapsed;
            AdminType.Visibility = Visibility.Visible;
            UserTypeSet.Visibility = Visibility.Visible;
            DevType.Visibility = Visibility.Visible;
        }

        private void AdminType_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserTypeBtn.Visibility = Visibility.Visible;
            AdminType.Visibility = Visibility.Collapsed;
            UserTypeSet.Visibility = Visibility.Collapsed;
            DevType.Visibility = Visibility.Collapsed;
            UserDatabase.SetUserType(EmployeeName.Text, "admin");
            UserType.Text = "admin";
        }

        private void DevType_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserTypeBtn.Visibility = Visibility.Visible;
            AdminType.Visibility = Visibility.Collapsed;
            UserTypeSet.Visibility = Visibility.Collapsed;
            DevType.Visibility = Visibility.Collapsed;
            UserDatabase.SetUserType(EmployeeName.Text, "dev");
            UserType.Text = "dev";
        }

        private void UserType_Click(object sender, RoutedEventArgs e)
        {
            ChangeUserTypeBtn.Visibility = Visibility.Visible;
            AdminType.Visibility = Visibility.Collapsed;
            UserTypeSet.Visibility = Visibility.Collapsed;
            DevType.Visibility = Visibility.Collapsed;
            UserDatabase.SetUserType(EmployeeName.Text, "user");
            UserType.Text = "user";
        }
    }
}
