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
        public MainWindow()
        {
            AccountLogin = new AccountLogin();
            AccountSignUp = new AccountSignUp();
            InitializeComponent();

            MainWindowFrame.Content = AccountLogin;

            AccountLogin.navigateToSignUpPageBtnClick += NavigateToSignUpPage;
            AccountSignUp.navigateToLoginPageBtnClick += NavigateToLoginPage;
        }

        public void NavigateToSignUpPage(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = AccountSignUp;
        }
        public void NavigateToLoginPage(object sender, RoutedEventArgs e)
        {
            MainWindowFrame.Content = AccountLogin;
        }
    }
}
