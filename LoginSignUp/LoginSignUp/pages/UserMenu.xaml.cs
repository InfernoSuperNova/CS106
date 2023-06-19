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
        static int projectCount = 0;

        public UserProject UserProject;
        public UserMenu()
        {
            InitializeComponent();
            header._NewProject += NewProject;
            for (; projectCount < 0; projectCount++)
            {
                // Create your dynamic objects
                var project = new UserProject();

                // Add the dynamic object to the stack panel
                ProjectField.Children.Add(project);
                project.ProjectTitle.Text = "Project " + projectCount;
            }
        }

        private void NewProject(object sender, RoutedEventArgs e)
        {
            //This button probably should not exist
            var project = new UserProject();
            ProjectField.Children.Add(project);
            project.ProjectTitle.Text = "Project " + projectCount++;
            //ProjectScrollField.ScrollToVerticalOffset(ProjectScrollField.ScrollableHeight);
            DelayedActionHelper.DelayedAction(DispatcherPriority.Background, TimeSpan.FromMilliseconds(0), () =>
            {
                ProjectScrollField.ScrollToVerticalOffset(ProjectScrollField.ScrollableHeight);


            });
        }

        private void SignOutBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
