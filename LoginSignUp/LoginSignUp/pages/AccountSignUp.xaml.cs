using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LoginSignUp.classes;
namespace LoginSignUp.pages
{
    /// <summary>
    /// Interaction logic for AccountSignUp.xaml
    /// </summary>
    public partial class AccountSignUp : Page
    {
        //login instead button
        public delegate void NavigateToLoginPageBtnClick(object sender, RoutedEventArgs e);
        public event NavigateToLoginPageBtnClick navigateToLoginPageBtnClick;
        //sign up successful
        public delegate void SuccessfulSignup(object sender, RoutedEventArgs e);
        public event SuccessfulSignup successfulSignup;

        public AccountSignUp()
        {
            InitializeComponent();
        }

        //Entrypoint functions
        private void NavigateLoginPage_Click(object sender, RoutedEventArgs e)
        {
            navigateToLoginPageBtnClick(sender, e);
        }

        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            SignUpProcess(sender, e);
        }
        //


        /// <summary>
        /// Handles the sign up process
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SignUpProcess(object sender, RoutedEventArgs e)
        {
            string inputUserName = SignUpUserName.Text;
            string inputPassword = SignUpPassword.Text;
            string[] lines = Helpers.ReadDataBase();
            var existingUsernameData = Helpers.EnumerateUserNames(lines);

            if (!CompareUsernameData(existingUsernameData, inputUserName, inputPassword)) { return; }

            WriteNewLogin(inputUserName, inputPassword, lines);

            successfulSignup(sender, e);
        }
        /// <summary>
        /// Checks if the UserName and Password meet a set of arbitrary conditions.
        /// </summary>
        /// <param name="existingUsernameData"></param>
        /// <param name="inputUserName"></param>
        /// <param name="inputPassword"></param>
        /// <returns>true if the UserName and Password meet all the conditions.</returns>
        private bool CompareUsernameData(List<string> existingUsernameData, string inputUserName, string inputPassword)
        {
            if (existingUsernameData.Contains(inputUserName))
            {
                MessageBox.Show("That username is taken!");
                SignUpPassword.Text = null;
                return false;
            }
            if (inputPassword == null || inputPassword == "")
            {
                MessageBox.Show("Please enter a valid password.");
                SignUpPassword.Text = null;
                return false;
            }
            if (inputPassword.Length < 5)
            {
                MessageBox.Show("Password must be at least 5 characters.");
                SignUpPassword.Text = null;
                return false;
            }
            MessageBox.Show("Username " + inputUserName + " successfully registered!");
            return true;
        }

        private void WriteNewLogin(string inputUserName, string inputPassword, string[] lines)
        {
            string addedData = $"{inputUserName}" + "," + $"{inputPassword}";
            lines = lines.Append(addedData).ToArray();
            File.WriteAllLines(@".\Database\UserAccountData.csv", lines);
            SignUpUserName.Text = null;
            SignUpPassword.Text = null;
        }
    }
}
