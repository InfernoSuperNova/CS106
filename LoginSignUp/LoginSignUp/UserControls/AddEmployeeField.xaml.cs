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
    /// Interaction logic for AddEmployeeField.xaml
    /// </summary>
    public partial class AddEmployeeField : UserControl
    {
        public AddEmployeeField()
        {
            InitializeComponent();
        }

        public event EventHandler CloseAddEmployeeField;

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            CloseAddEmployeeField?.Invoke(this, e);
        }
    }
}
