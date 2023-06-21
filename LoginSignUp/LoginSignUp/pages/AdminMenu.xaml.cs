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

        private string[] lines;
        private List<AdminProject> adminProjects;
        NewProject newProject;
        public AdminMenu()
        {
            adminProjects = new List<AdminProject>();
            InitializeComponent();
            header._NewProject += NewProject;
            header._SignOut += SignOut;
            lines = ProjectDataBase.ReadNoHeader();
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
        }
        private void AddNewProjectToList(object sender, RoutedEventArgs e, string projectName)
        {
            ProjectField.Children.Remove(newProject);
            newProject = null;

            AdminProject newAdminProject = new AdminProject();
            newAdminProject.ProjectTitle.Text = projectName;
            ProjectField.Children.Add(newAdminProject);
            lines = lines.Append(projectName).ToArray();
            ProjectList.Write(lines);
        }

        private void AdminProject_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public delegate void SignOutMain(object sender, RoutedEventArgs e);
        public event SignOutMain _SignOut;
        private void SignOut(object sender, RoutedEventArgs e)
        {
            _SignOut(sender, e);
        }
    }
}
