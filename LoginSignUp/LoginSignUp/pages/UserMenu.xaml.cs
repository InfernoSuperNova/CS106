using LoginSignUp.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using LoginSignUp.classes;
namespace LoginSignUp.pages
{
    /// <summary>
    /// Interaction logic for UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Page
    {
        private string[] lines;
        private int projectCount = 0;
        public UserMenu()
        {
            InitializeComponent();
            header._SignOut += SignOut;
            header.AddNewProjectBtn.Visibility = Visibility.Hidden;
            lines = ProjectList.Read();
            
            foreach (string name in lines)
            {
                //store this project somewhere, otherwise it'll cause a memory leak
                var project = new UserProject();

                // Add the dynamic object to the stack panel
                ProjectField.Children.Add(project);
                project.ProjectTitle.Text = name;
                projectCount++;
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (projectCount != 0) { return; }
            DelayedActionHelper.DelayedAction(DispatcherPriority.Background, TimeSpan.FromMilliseconds(0), () =>
            {
                MessageBox.Show("There are no projects available, contact your system administrator!");
            });
        }
        public delegate void SignOutMain(object sender, RoutedEventArgs e);
        public event SignOutMain _SignOut;
        private void SignOut(object sender, RoutedEventArgs e)
        {
            _SignOut(sender, e);
        }
    }
}

