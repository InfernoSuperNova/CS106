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

namespace LoginSignUp.UserControls
{
    /// <summary>
    /// Interaction logic for AdminBugEdit.xaml
    /// </summary>
    public partial class AdminBugEdit : UserControl
    {
        public AdminBugEdit()
        {
            InitializeComponent();
        }

        private void AdminProject_Loaded(object sender, RoutedEventArgs e)
        {
       
        }

        private void DropDownMenu_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            // Show or hide the dropdown when the "More" option is clicked
            if (popup.IsOpen)
            {
                popup.IsOpen = false;
            }
            else
            {
                popup.IsOpen = true;
            }
        }
    }
}
