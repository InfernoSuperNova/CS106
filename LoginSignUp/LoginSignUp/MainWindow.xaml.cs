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
        public MainWindow()
        {
            AccountLogin = new AccountLogin();
            AccountSignUp = new AccountSignUp();
            UserMenu = new UserMenu();
            InitializeComponent();

            MainWindowFrame.Content = AccountLogin;

            AccountLogin.navigateToSignUpPageBtnClick += PageSignUp;
            AccountSignUp.navigateToLoginPageBtnClick += PageLogin;
            AccountSignUp.successfulSignup += PageUserMenu;
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
    }
}
