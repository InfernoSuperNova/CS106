using LoginSignUp.classes;
using LoginSignUp.UserControls;
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
using System.Windows.Threading;

namespace LoginSignUp.pages
{
    /// <summary>
    /// Interaction logic for AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Page
    {
        static int projectCount = 0;
        public AdminMenu()
        {
            InitializeComponent();
            string[] lines = ProjectList.Read();
            foreach (string name in lines)
            {
                //store this project somewhere, otherwise it'll cause a memory leak
                var project = new AdminProject();

                // Add the dynamic object to the stack panel
                ProjectField.Children.Add(project);
                project.ProjectTitle.Text = name;
            }
        }

        private void NewProjectBtn_Click(object sender, RoutedEventArgs e)
        {
            var project = new AdminProject();
            ProjectField.Children.Add(project);
            project.ProjectTitle.Text = "Project " + projectCount++;
            //ProjectScrollField.ScrollToVerticalOffset(ProjectScrollField.ScrollableHeight);
            DelayedActionHelper.DelayedAction(DispatcherPriority.Background, TimeSpan.FromMilliseconds(0), () =>
            {
                ProjectScrollField.ScrollToVerticalOffset(ProjectScrollField.ScrollableHeight);


            });
        }
    }
}
