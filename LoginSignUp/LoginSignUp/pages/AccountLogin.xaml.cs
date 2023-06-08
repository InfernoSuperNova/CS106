using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AccountLogin.xaml
    /// </summary>
    public partial class AccountLogin : Page
    {
        public delegate void NavigateToSignUpPageBtnClick(object sender, RoutedEventArgs e);
        public event NavigateToSignUpPageBtnClick navigateToSignUpPageBtnClick;

        public AccountLogin()
        {
            InitializeComponent();
        }

        private void NavigateSignUpPage_Click(object sender, RoutedEventArgs e)
        {
            navigateToSignUpPageBtnClick(sender, e);
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputUserName = LoginUserName.Text;
            string inputPassword = LoginPassword.Text;

            string[] lines = File.ReadAllLines(@".\Database\UserAccountData.csv");
            var existingUserNameData = lines.Skip(1).Select(row => row.Split(',')[0]).ToList();
            var existingPasswordData = lines.Skip(1).Select(row => row.Split(',')[1]).ToList();

            if(inputUserName == "" || inputPassword == "")
            {
                MessageBox.Show("Please enter both username and password");
            }
            else if (existingUserNameData.Contains(inputUserName) && existingPasswordData.Contains(inputPassword))
            {
                MessageBox.Show("Successful login");
            }
            else
            {
                MessageBox.Show("Username or password incorrect");
            }
        }

        
    }
}
