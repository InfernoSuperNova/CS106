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

        }


    }
}
