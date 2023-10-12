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
using static LoginSignUp.UserControls.AddBug;
using static LoginSignUp.UserControls.Bug;

namespace LoginSignUp.UserControls
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {

        
        public Header()
        {
            InitializeComponent();
        }

        public delegate void NewProject(object sender, RoutedEventArgs e);
        public event NewProject _NewProject;
        private void AddNewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            _NewProject(sender, e);
        }

        public delegate void SignOut(object sender, RoutedEventArgs e);
        public event SignOut _SignOut;
        private void SignOutBtn_Click(object sender, RoutedEventArgs e)
        {
            _SignOut(sender, e);
        }
        public void SetUserType(string type)
        {
            if (type == "user")
            {
                AddNewProjectBtn.Visibility = Visibility.Collapsed;
            }
            else if (type == "dev")
            {
                AddNewProjectBtn.Visibility = Visibility.Collapsed;
            }
            else if (type == "admin")
            {
                AddNewProjectBtn.Visibility = Visibility.Visible;
            }
        }
    }
}
