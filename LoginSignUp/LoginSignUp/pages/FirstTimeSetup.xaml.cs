﻿using System.Collections.Generic;
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
    public partial class FirstTimeSetup : Page
    {
        //sign up successful
        public delegate void SuccessfulSignup(object sender, RoutedEventArgs e, string UserType, string UserName);
        public event SuccessfulSignup _SuccessfulSignup;

        public FirstTimeSetup()
        {
            InitializeComponent();
        }

        //Entrypoint functions
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
            string[] lines = UserDatabase.Read();

            if (!CompareUsernameData(inputUserName, inputPassword)) { return; }

            WriteNewLogin(inputUserName, inputPassword, lines);

            _SuccessfulSignup(sender, e, "admin", inputUserName);
        }
        /// <summary>
        /// Checks if the UserName and Password meet a set of arbitrary conditions.
        /// </summary>
        /// <param name="existingUsernameData"></param>
        /// <param name="inputUserName"></param>
        /// <param name="inputPassword"></param>
        /// <returns>true if the UserName and Password meet all the conditions.</returns>
        private bool CompareUsernameData(string inputUserName, string inputPassword)
        {
            if (string.IsNullOrEmpty(inputPassword))
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
            if (!UserInput.ValidateString(inputUserName))
            {
                SignUpPassword.Text = null;
                return false;
            }
            if (!UserInput.ValidateString(inputPassword))
            {
                SignUpPassword.Text = null;
                return false;
            }
            MessageBox.Show("Administrator " + inputUserName + " successfully registered!");
            return true;
        }
        /// <summary>
        /// Writes a new UserName/Password pair to the database.
        /// </summary>
        /// <param name="inputUserName"></param>
        /// <param name="inputPassword"></param>
        /// <param name="lines"></param>
        private void WriteNewLogin(string inputUserName, string inputPassword, string[] lines)
        {
            string addedData = $"{inputUserName}" + "," + $"{inputPassword}" + ",admin";
            lines = lines.Append(addedData).ToArray();
            File.WriteAllLines(@".\Database\UserAccountData.csv", lines);
            SignUpUserName.Text = null;
            SignUpPassword.Text = null;
        }
    }
}
