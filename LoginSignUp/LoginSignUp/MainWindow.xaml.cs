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
using LoginSignUp.classes;
using LoginSignUp.UserControls;

namespace LoginSignUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FirstTimeSetup FirstTimeSetup;
        public AccountLogin AccountLogin;
        public AccountSignUp AccountSignUp;
        public MainMenu MainMenu;
        public ManageEmployees ManageEmployees;

        public MainWindow()
        {
            InitializeComponent();
            InitializeFiles.CreateStructure();
            AccountLogin = new AccountLogin();
            AccountSignUp = new AccountSignUp();
            MainMenu = new MainMenu();
            ManageEmployees = new ManageEmployees();
            MainWindowFrame.Content = AccountLogin;
            if (!UserDatabase.IsDatabaseContainData())
            {
                FirstTimeSetup = new FirstTimeSetup();
                FirstTimeSetup._SuccessfulSignup += SignUpSuccess;
                MainWindowFrame.Content = FirstTimeSetup;
            }
            

            

            AccountLogin._NavigateToSignUpPageBtnClick += PageSignUp;
            AccountLogin._SuccessfulLogin += GetUserAccountType;
            AccountSignUp._NavigateToLoginPageBtnClick += PageLogin;
            AccountSignUp._SuccessfulSignup += SignUpSuccess;
            MainMenu._SignOut += PageLogin;
            MainMenu._AdminManageUsers += PageManageUsers;
            ManageEmployees._ReturnToMenu += PageMenu;

            ProjectDataBase.InitializeDatabase();
        }
        public void GetUserAccountType(object sender, RoutedEventArgs e, List<string> UserNames, string UserName)
        {
            int index = UserDatabase.GetUserIndexByUserName(UserNames, UserName);
            string userType = UserDatabase.GetUserTypeByIndex(index);
            SetUserDetails(sender, e, userType, UserName);
            PageMenu(sender, e);
        }
        private void SignUpSuccess(object sender, RoutedEventArgs e, string userType, string UserName)
        {
            PageLogin(sender, e);
        }
        private void SetUserDetails(object sender, RoutedEventArgs e, string userType, string UserName)
        {
            MainMenu.userName = UserName;
            switch (userType)
            {
                case "user":
                    MainMenu.userType = "user";
                    MainMenu.header.UserText.Text = UserName + " : User";
                    break;
                case "admin":
                    MainMenu.userType = "admin";
                    MainMenu.header.UserText.Text = UserName + " : Admin";
                    break;
                case "dev":
                    MainMenu.userType = "dev";
                    MainMenu.header.UserText.Text = UserName + " : Dev";
                    break;
            }
        }
        public void PageSignUp(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = AccountSignUp;
        }
        public void PageLogin(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = AccountLogin;
        }
        public void PageMenu(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = MainMenu;
        }
        private void PageManageUsers(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = ManageEmployees;
        }
    }
}
