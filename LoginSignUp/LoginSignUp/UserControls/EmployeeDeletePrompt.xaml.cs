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
    /// Interaction logic for EmployeeDeletePrompt.xaml
    /// </summary>
    public partial class EmployeeDeletePrompt : UserControl
    {
        public EmployeeDeletePrompt()
        {
            InitializeComponent();
        }
        public delegate void CancelDelete(object sender, RoutedEventArgs e);
        public event CancelDelete _CancelDelete;
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            _CancelDelete(sender, e);
        }
        public delegate void ConfirmDelete(object sender, RoutedEventArgs e);
        public event ConfirmDelete _ConfirmDelete;
        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            _ConfirmDelete(sender, e);
        }
    }
}
