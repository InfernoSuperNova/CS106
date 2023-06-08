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
    /// Interaction logic for AccountSignUp.xaml
    /// </summary>
    public partial class AccountSignUp : Page
    {
        public delegate void NavigateToLoginPageBtnClick(object sender, RoutedEventArgs e);
        public event NavigateToLoginPageBtnClick navigateToLoginPageBtnClick;
        public AccountSignUp()
        {
            InitializeComponent();
        }

        private void NavigateLoginPage_Click(object sender, RoutedEventArgs e)
        {
            navigateToLoginPageBtnClick(sender, e);
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            string inputUserName = SignUpUserName.Text;
            string inputPassword = SignUpPassword.Text;
            
            string[] lines = File.ReadAllLines(@".\Database\UserAccountData.csv");

            var existingUsernameData = lines.Skip(1).Select(row => row.Split(',')[0]).ToList();

            if (existingUsernameData.Contains(inputUserName))
            {
                MessageBox.Show("That username is taken!");
                SignUpPassword.Text = null;
                return;
            }
            if (inputPassword == null || inputPassword == "")
            {
                MessageBox.Show("Please enter a valid password.");
                SignUpPassword.Text = null;
                return;
            }
            if (inputPassword.Length < 5)
            {
                MessageBox.Show("Password must be at least 5 characters.");
                SignUpPassword.Text = null;
                return;
            }
            MessageBox.Show("Username " + inputUserName + " successfully registered!");

            string addedData = $"{inputUserName}" + "," + $"{inputPassword}";
            lines = lines.Append(addedData).ToArray();
            File.WriteAllLines(@".\Database\UserAccountData.csv", lines);
            SignUpUserName.Text = null;
            SignUpPassword.Text = null;

        }

        //Test Change
    }
}
