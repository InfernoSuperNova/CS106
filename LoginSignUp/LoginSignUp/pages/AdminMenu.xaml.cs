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
            header._NewProject += NewProject;
            string[] lines = ProjectList.Read();
            foreach (string name in lines)
            {
                //store this project somewhere, otherwise it'll cause a memory leak
                var project = new AdminProject();

                // Add the dynamic object to the stack panel
                ProjectField.Children.Add(project);
                project.ProjectTitle.Text = name;
                project._DeleteProject += DeleteProject;
                adminProjects.Add(project);
            }
        }

        private void NewProject(object sender, RoutedEventArgs e)
        {
            //If a new project is already being made, then clear
            if (newProject != null) { return; };
            newProject = new NewProject();
            ProjectField.Children.Add(newProject);

            newProject._AddNewProject += AddNewProjectToList;
            //ProjectScrollField.ScrollToVerticalOffset(ProjectScrollField.ScrollableHeight);
            DelayedActionHelper.DelayedAction(DispatcherPriority.Background, TimeSpan.FromMilliseconds(0), () =>
            {
                ProjectScrollField.ScrollToVerticalOffset(ProjectScrollField.ScrollableHeight);
            });
        }

        private void DeleteProject(object sender, RoutedEventArgs e, string name)
        {
            //Gets a pointer to the admin project element
            AdminProject projectToRemove = adminProjects.Find(project => project.ProjectTitle.Text == name);
            //Remove that from memory
            adminProjects.Remove(projectToRemove);
            ProjectField.Children.Remove(projectToRemove);

            //Create a new string
            string[] newLines = new string[0];
            //Fill that with all of lines, sans name of project being removed
            foreach (string oldName in lines)
            {
                if (name == oldName) { continue; }
                newLines = newLines.Append(oldName).ToArray();
            }
            //Overwrite lines
            lines = newLines;
            //Overwrite the database
            ProjectList.Write(lines);
        }

            });
        }

        private void AdminProject_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
