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
    /// Interaction logic for UserProject.xaml
    /// </summary>
    public partial class UserProject : UserControl
    {
        public UserProject()
        {
            InitializeComponent();
        }
        public delegate void AddBugProject(object sender, RoutedEventArgs e, string projectName);
        public event AddBugProject _AddBugProject;

        private void AddBugBtn_Click(object sender, RoutedEventArgs e)
        {
            _AddBugProject(sender, e, ProjectTitle.Text);
        }
    }

}
