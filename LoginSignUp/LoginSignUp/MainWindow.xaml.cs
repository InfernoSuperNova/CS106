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
namespace LoginSignUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AccountLogin AccountLogin;
        public AccountSignUp AccountSignUp;
        public UserMenu UserMenu;
        public AdminMenu AdminMenu;
        public MainWindow()
        {
            AccountLogin = new AccountLogin();
            AccountSignUp = new AccountSignUp();
            UserMenu = new UserMenu();
            AdminMenu = new AdminMenu();
            InitializeComponent();

            MainWindowFrame.Content = AccountLogin;

            AccountLogin._NavigateToSignUpPageBtnClick += PageSignUp;
            AccountLogin._SuccessfulLogin += GetUserAccountType;
            AccountSignUp._NavigateToLoginPageBtnClick += PageLogin;
            AccountSignUp._SuccessfulSignup += PageUserMenu;
        }
        public void GetUserAccountType(object sender, RoutedEventArgs e, List<string> UserNames, string UserName)
        {
            int index = UserDatabase.GetUserIndexByUserName(UserNames, UserName);
            string userType = UserDatabase.GetUserTypeByIndex(index);
            switch (userType)
            {
                case "user":
                    PageUserMenu(sender, e);
                    break;
                case "admin":
                    PageAdminMenu(sender, e);
                    break;
                case "dev":
                    PageAdminMenu(sender, e);
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
        public void PageUserMenu(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = UserMenu;
        }
        public void PageAdminMenu(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = AdminMenu;
        }
    }
}
