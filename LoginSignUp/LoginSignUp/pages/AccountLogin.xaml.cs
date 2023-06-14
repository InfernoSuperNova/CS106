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
using LoginSignUp.classes;
namespace LoginSignUp.pages
{
    /// <summary>
    /// Interaction logic for AccountLogin.xaml
    /// </summary>
    public partial class AccountLogin : Page
    {
        //sign up instead button
        public delegate void NavigateToSignUpPageBtnClick(object sender, RoutedEventArgs e);
        public event NavigateToSignUpPageBtnClick _NavigateToSignUpPageBtnClick;
        //login successful
        public delegate void SuccessfulLogin(object sender, RoutedEventArgs e, List<string> UserNames, string UserName);
        public event SuccessfulLogin _SuccessfulLogin;
        
        public AccountLogin()
        {
            InitializeComponent();
        }
        //Entrypoint functions
        private void NavigateSignUpPage_Click(object sender, RoutedEventArgs e)
        {
            _NavigateToSignUpPageBtnClick(sender, e);
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginProcess(sender, e);
        }

        //



        private void LoginProcess(object sender, RoutedEventArgs e)
        {
            string inputUserName = LoginUserName.Text;
            string inputPassword = LoginPassWord.Text;

            string[] lines = UserDatabase.ReadDatabase();
            var existingUserNameData = UserDatabase.EnumerateUserNames(lines);
            var existingPasswordData = UserDatabase.EnumeratePasswords(lines);

            if (LoginChecks(inputUserName, inputPassword, existingUserNameData, existingPasswordData))
            {
                //next steps here
                LoginUserName.Text = null;
                LoginPassWord.Text = null;
                _SuccessfulLogin(sender, e, existingUserNameData, inputUserName);
            }           
           
        }
        private bool LoginChecks(string inputUserName, string inputPassword, List<string> existingUserNameData, List<string> existingPasswordData)
        {
            if (inputUserName == "" || inputPassword == "")
            {
                MessageBox.Show("Please enter both username and password");
                return false;
            }
            //may need to add an else here if guard clause is removed for whatever reason
            if (UserDatabase.CheckLogin(existingUserNameData, existingPasswordData, inputUserName, inputPassword))
            {
                MessageBox.Show("Successful login");
                return true;
            }
            else
            {
                MessageBox.Show("Username or password incorrect");
                return false;
            }            
        }
    }
}
