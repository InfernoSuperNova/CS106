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
        private List<Employee> employees;
        AddEmployee addEmployee;
        NewProject newProject;
        public MainMenu()
        {

            InitializeComponent();


            projects = new List<Project>();
            openBugs = new List<Bug>();
            employees = new List<Employee>();
            AddBugMenu._AddBugBtn += AddBug;
            header._NewProject += NewProject;
            header._SignOut += SignOut;
            header._ManageUsers += AdminManageUsers;
            
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
                project._ToggleEmployees += OpenUserManagementMenu;
                //Store the project in array
                projects.Add(project);

            }

        }

        public delegate void AdminMainManageUsers(object sender, RoutedEventArgs e);
        public event AdminMainManageUsers _AdminManageUsers;
        private void AdminManageUsers(object sender, RoutedEventArgs e)
        {
            _AdminManageUsers(sender, e);
        }
        //visability toggle run whatever code here
        private void OpenUserManagementMenu(object sender, EventArgs e, string project)
        {
            
            SetVisible(Fields.EmployeeList);
            EmployeeContainer.Children.Clear();
            string[] users = UserDatabase.GetAssignedUsers(project);
            foreach (string user in users)
            {
                Employee employee = new Employee();
                employee.EmployeeName.Text = user;
                employee._DeleteUser += RemoveUserFromProject;
                employees.Add(employee);
                EmployeeContainer.Children.Add(employee);
            }
            addEmployee = new AddEmployee();
            addEmployee._ConfirmAddUserMaster += AddUserToProject;
            EmployeeContainer.Children.Add(addEmployee);
            currentProject = project;
        }
        private void AddUserToProject(object sender, RoutedEventArgs e, string userName)
        {
            int result = UserDatabase.UpdateUserResponsibilities(userName, currentProject, true);
            switch(result)
            {
                case 0:
                    EmployeeContainer.Children.Remove(addEmployee);
                    Employee employee = new Employee();
                    employee.EmployeeName.Text = userName;
                    employee._DeleteUser += RemoveUserFromProject;
                    employees.Add(employee);
                    EmployeeContainer.Children.Add(employee);
                    EmployeeContainer.Children.Add(addEmployee);
                    break;
                case 1:
                    MessageBox.Show("Couldn't find a user with that name!");
                    break;
                case 2:
                    MessageBox.Show("Couldn't find a project with that name for the user!");
                    break;
            }
        }
        private void RemoveUserFromProject(object sender, RoutedEventArgs e, string userName)
        {
            Employee employeeToRemove = employees.Find(employee => employee.EmployeeName.Text == userName);
            //Remove that from memory
            employees.Remove(employeeToRemove);
            EmployeeContainer.Children.Remove(employeeToRemove);
            UserDatabase.UpdateUserResponsibilities(userName, currentProject, false);
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
                Bug bugEdit = new Bug(bug, projectName);
                bugEdit.SetUserType(userType);
                bugEdit._Delete += DeleteBug;
                BugContainer.Children.Add(bugEdit);
                openBugs.Add(bugEdit);
            }
        }

        private void DeleteBug(object sender, RoutedEventArgs e, string reference)
        {

            ProjectDataBase.Bugs.DeleteBug(currentProject, reference);
            Bug bugToRemove = openBugs.Find(project => project.bugDetails.reference == reference);
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
            EmployeeList.Visibility = Visibility.Hidden;
            AddEmployeeBtn.Visibility = Visibility.Hidden;
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
                case Fields.EmployeeList:
                    PrimaryDropdown.Visibility = Visibility.Hidden;
                    EmployeeList.Visibility = Visibility.Visible;
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
            EmployeeList,
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
