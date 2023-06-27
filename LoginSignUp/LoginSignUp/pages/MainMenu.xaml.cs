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
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {

        public string userType = "";
        private string currentProject = "";
        private string[] lines;
        private List<Project> projects;
        private List<Bug> openBugs;
        NewProject newProject;
        public MainMenu()
        {

            InitializeComponent();


            projects = new List<Project>();
            openBugs = new List<Bug>();
            AddBugMenu._AddBugBtn += AddBug;
            header._NewProject += NewProject;
            header._SignOut += SignOut;
            lines = ProjectDataBase.ReadNoHeader();
            foreach (string name in lines)
            {
                var project = new Project();

                // Add the dynamic object to the stack panel
                ProjectField.Children.Add(project);
                project.ProjectTitle.Text = name;
                project.ActiveBugs.Text = "Active Bugs: " + ProjectDataBase.Bugs.GetBugCount(name).ToString();
                project._DeleteProject += DeleteProject;
                project._EditBugMenu += OpenEditBugMenu;
                project._AddBugProject += OpenNewBugMenu;
                project._ToggleEmployees += Testing_ToggleVisibility;
                //Store the project in array
                projects.Add(project);

            }

        }


        //visability toggle run whatever code here
        private void Testing_ToggleVisibility(object sender, EventArgs e, string project)
        {
            SetVisible(Fields.ExampleToggle);
        }
        private void ExampleToggle_Click(object sender, RoutedEventArgs e)
        {
            //ProjectDataBase.Bugs.DeleteBug("GunGame", 2);
            //ExampleToggle.Visibility = Visibility.Hidden;
            //AddEmployee.Visibility = Visibility.Visible;
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
            Project newAdminProject = new Project();
            newAdminProject.ProjectTitle.Text = projectName;
            newAdminProject._DeleteProject += DeleteProject;
            newAdminProject._EditBugMenu += OpenEditBugMenu;
            newAdminProject._AddBugProject += OpenNewBugMenu;
            //Add the project to the list, and the UI
            projects.Add(newAdminProject);
            ProjectField.Children.Add(newAdminProject);
            //Add the project to the database
            lines = lines.Append(projectName).ToArray();
            ProjectDataBase.Write(lines.Prepend("Project Name").ToArray());
            //Generate the files associated with the project
            ProjectDataBase.CreateProject(projectName);
        }

        private void DeleteProject(object sender, RoutedEventArgs e, string name)
        {
            ProjectDataBase.DeleteProject(name);
            //Gets a pointer to the admin project element
            Project projectToRemove = projects.Find(project => project.ProjectTitle.Text == name);
            //Remove that from memory
            projects.Remove(projectToRemove);
            ProjectField.Children.Remove(projectToRemove);
            HideAll();
        }

        public delegate void SignOutMain(object sender, RoutedEventArgs e);
        public event SignOutMain _SignOut;
        private void SignOut(object sender, RoutedEventArgs e)
        {
            _SignOut(sender, e);
        }
        private void OpenNewBugMenu(object sender, RoutedEventArgs e, string projectName)
        {
            HideAll();
            OptionHint.Visibility = Visibility.Hidden;
            AddBugMenu.Enable(projectName);
        }


        private void OpenEditBugMenu(object sender, RoutedEventArgs e, string projectName)
        {
            CloseEditBugMenu();
            currentProject = projectName;
            PrimaryDropdown.Visibility = Visibility.Hidden;
            BugList.Visibility = Visibility.Visible;
            
            //Populate the bug list with bugs


            List<ProjectDataBase.Bugs.Bug> bugs = ProjectDataBase.Bugs.GetBugList(projectName);

            foreach (ProjectDataBase.Bugs.Bug bug in bugs)
            {
                Bug bugEdit = new Bug(bug.reference);
                bugEdit.Title.Text = bug.name;
                bugEdit.Description.Text = bug.description;
                bugEdit.TimeSpent.Text = "Time Spent: " + bug.timeSpent;
                bugEdit.Priority.Text = "Priority: " + bug.priority;
                bugEdit.StepsToReproduce.Text = "Steps to reproduce: " + bug.stepsToReproduce;
                bugEdit.SetUserType(userType);
                bugEdit._Delete += DeleteBug;
                BugContainer.Children.Add(bugEdit);
                openBugs.Add(bugEdit);
            }
        }

        private void DeleteBug(object sender, RoutedEventArgs e, string reference)
        {

            ProjectDataBase.Bugs.DeleteBug(currentProject, reference);
            Bug bugToRemove = openBugs.Find(project => project.reference == reference);
            BugContainer.Children.Remove(bugToRemove);
            openBugs.Remove(bugToRemove);
            UpdateProjectBugCount(currentProject);
        }
        private void CloseEditBugMenu()
        {
            currentProject = "";
            BugContainer.Children.Clear();
            openBugs.Clear();
        }
        private void AddBug(object sender, RoutedEventArgs e, ProjectDataBase.Bugs.Bug bug, string projectName)
        {
            ProjectDataBase.Bugs.CreateBug(projectName, bug);
            OpenEditBugMenu(sender, e, projectName);
            UpdateProjectBugCount(projectName);
        }

        private void UpdateProjectBugCount(string projectName)
        {
            Project projectToUpdate = projects.Find(project => project.ProjectTitle.Text == projectName);
            int bugCount = ProjectDataBase.Bugs.GetBugCount(projectName);
            projectToUpdate.ActiveBugs.Text = "Active Bugs: " + bugCount;
        }
        private void HideAll()
        {
            CloseEditBugMenu();
            AddEmployee.Visibility = Visibility.Hidden;
            AddEmployeeBtn.Visibility = Visibility.Hidden;
            ExampleToggle.Visibility = Visibility.Hidden;
            BugList.Visibility = Visibility.Hidden;
            AddBugMenu.Disable();
            OptionHint.Visibility = Visibility.Visible;
            PrimaryDropdown.Visibility = Visibility.Visible;
        }
        private void SetVisible(Fields field)
        {
            HideAll();
            OptionHint.Visibility = Visibility.Hidden;
            switch (field)
            {
                case Fields.AddBug:
                    // Use AddBug's specific initializer instead
                    break;
                case Fields.EditBug:
                    PrimaryDropdown.Visibility = Visibility.Hidden;
                    BugList.Visibility = Visibility.Visible;
                    break;
                case Fields.AddEmployeeBtn:
                    AddEmployeeBtn.Visibility = Visibility.Visible;
                    break;
                case Fields.AddEmployee:
                    AddEmployee.Visibility = Visibility.Visible;
                    break;
                case Fields.ExampleToggle:
                    ExampleToggle.Visibility = Visibility.Visible;
                    break;
                default:
                    // Handle other cases or throw an exception if necessary
                    break;
            }
        }
        enum Fields
        {
            AddBug,
            EditBug,
            AddEmployeeBtn,
            AddEmployee,
            ExampleToggle,
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            header.SetUserType(userType);
            foreach (Project project in projects)
            {
                project.SetUserType(userType);
            }
            HideAll();
        }
    }
}