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
            AddBugMenu._AddBugBtn += AddBug;
            lines = ProjectDataBase.ReadNoHeader();

            foreach (string name in lines)
            {
                //store this project somewhere, otherwise it'll cause a memory leak
                var project = new UserProject();

                // Add the dynamic object to the stack panel
                ProjectField.Children.Add(project);
                project.ProjectTitle.Text = name;
                project._AddBugProject += OpenBugMenu;
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

        private void Testing_ToggleVisibility(object sender, EventArgs e, string project)
        {
            SetVisible(Fields.ExampleToggle);
        }

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

        private void AddNewProjectToList(object sender, RoutedEventArgs e, string projectName)
        {
            //destroy the placeholder project
            ProjectField.Children.Remove(newProject);
            newProject = null;

            //Define new project
            UserProject newUserProject = new UserProject();
            newUserProject.ProjectTitle.Text = projectName;
            newUserProject._DeleteProject += DeleteProject;
            newUserProject._EditBugMenu += OpenEditBugMenu;
            newUserProject._AddBugProject += OpenNewBugMenu;
            //Add the project to the list, and the UI
            userProjects.Add(newUserProject);
            ProjectField.Children.Add(newUserProject);
            //Add the project to the database
            lines = lines.Append(projectName).ToArray();
            ProjectDataBase.Write(lines.Prepend("Project Name").ToArray());
            //Generate the files associated with the project
            ProjectDataBase.CreateProject(projectName);
        }

        private void OpenBugMenu(object sender, RoutedEventArgs e, string projectName)
        {
            AddBugMenu.Enable(projectName);
            OptionHint.Visibility = Visibility.Hidden;
        }

        private void OpenNewBugMenu(object sender, RoutedEventArgs e, string projectName)
        {
            HideAll();
            OptionHint.Visibility = Visibility.Hidden;
            AddBugMenu.Enable(projectName);
        }

        private void AddBug(object sender, RoutedEventArgs e, ProjectDataBase.Bugs.Bug bug, string projectName)
        {
            ProjectDataBase.Bugs.CreateBug(projectName, bug);
            OpenEditBugMenu(sender, e, projectName);
            UpdateProjectBugCount(projectName);
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
                UserBugEdit bugEdit = new UserBugEdit(bug.reference);
                bugEdit.Title.Text = bug.name;
                bugEdit.Description.Text = bug.description;
                bugEdit.TimeSpent.Text = "Time Spent: " + bug.timeSpent;
                bugEdit.Priority.Text = "Priority: " + bug.priority;
                bugEdit.StepsToReproduce.Text = "Steps to reproduce: " + bug.stepsToReproduce;
                bugEdit._Delete += DeleteBug;
                BugContainer.Children.Add(bugEdit);
                openBugs.Add(bugEdit);
            }
        }

        private void CloseEditBugMenu()
        {
            currentProject = "";
            BugContainer.Children.Clear();
            openBugs.Clear();
        }

        private void UpdateProjectBugCount(string projectName)
        {
            UserProject projectToUpdate = userProjects.Find(project => project.ProjectTitle.Text == projectName);
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
    }
}

