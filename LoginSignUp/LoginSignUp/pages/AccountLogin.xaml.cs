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
            string inputPassword = LoginPassWord.Text;

            string[] lines = File.ReadAllLines(@".\Database\UserAccountData.csv");
            var existingUserNameData = lines.Skip(1).Select(row => row.Split(',')[0]).ToList();
            var existingPasswordData = lines.Skip(1).Select(row => row.Split(',')[1]).ToList();

            if(inputUserName == "" || inputPassword == "")
            {
                MessageBox.Show("Please enter both username and password");
                return;
            }
            //may need to add an else here if guard clause is removed for whatever reason
            for (int i = 0; i < existingUserNameData.Count(); i++)
            {
                if (inputUserName == existingUserNameData.ElementAt(i) && inputPassword == existingPasswordData.ElementAt(i))
                {
                    MessageBox.Show("Successful login");
                    return;
                }
            }
            //Will only get here if it doesn't match and isn't empty
            MessageBox.Show("Username or password incorrect");
        }

        
    }
}
