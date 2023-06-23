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
                var project = new AdminProject();

                // Add the dynamic object to the stack panel
                ProjectField.Children.Add(project);
                project.ProjectTitle.Text = name;
                project._DeleteProject += DeleteProject;
                project._AddBugProject += AddBugProject;
                //Store the project in array
                adminProjects.Add(project);
            }
        }
        //Creates a "New Project" object that lets you put in the name and confirm
        private void NewProject(object sender, RoutedEventArgs e)
        {
            //Don't go any further if a new project object already exists
            if (newProject != null) { return; };
            //Create a new project object and put it into the UI
            newProject = new NewProject();
            ProjectField.Children.Add(newProject);
            //Define callback for finalizing the construction of the project
            newProject._AddNewProject += AddNewProjectToList;
            //Trigger scroll with minimal delay (effective delay of 1 frame)
            DelayedActionHelper.DelayedAction(DispatcherPriority.Background, TimeSpan.FromMilliseconds(0), () =>
            {
                ProjectScrollField.ScrollToVerticalOffset(ProjectScrollField.ScrollableHeight);
            });
        }
        //Adds a new project to the database, array, and UI, and creates the necessary file structure
        private void AddNewProjectToList(object sender, RoutedEventArgs e, string projectName)
        {
            //destroy the placeholder project
            ProjectField.Children.Remove(newProject);
            newProject = null;

            //Define new project
            AdminProject newAdminProject = new AdminProject();
            newAdminProject.ProjectTitle.Text = projectName;
            newAdminProject._DeleteProject += DeleteProject;
            newAdminProject._AddBugProject += AddBugProject;
            //Add the project to the list, and the UI
            adminProjects.Add(newAdminProject);
            ProjectField.Children.Add(newAdminProject);
            //Add the project to the database
            lines = lines.Append(projectName).ToArray();
            ProjectDataBase.Write(lines);
            //Generate the files associated with the project
            ProjectDataBase.CreateProject(projectName);
        }

        private void DeleteProject(object sender, RoutedEventArgs e, string name)
        {
            ProjectDataBase.DeleteProject(name);
            //Gets a pointer to the admin project element
            AdminProject projectToRemove = adminProjects.Find(project => project.ProjectTitle.Text == name);
            //Remove that from memory
            adminProjects.Remove(projectToRemove);
            ProjectField.Children.Remove(projectToRemove);
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

        private void AddBugProject(object sender, RoutedEventArgs e, string projectName)
        {
            MessageBox.Show(projectName + " is triggering add bug project!");
        }
    }
}
