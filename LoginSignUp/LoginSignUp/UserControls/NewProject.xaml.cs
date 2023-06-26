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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class NewProject : UserControl
    {

       

        public NewProject()
        {
            InitializeComponent();
        }

        public delegate void AddNewProject(object sender, RoutedEventArgs e, string projectName);
        public event AddNewProject _AddNewProject;
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            //string name = ProjectInputText.Text;
            //_AddNewProject(sender, e, name);
        }
    }
}
