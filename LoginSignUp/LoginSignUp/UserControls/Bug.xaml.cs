using LoginSignUp.classes;
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
using static LoginSignUp.UserControls.AddBug;

namespace LoginSignUp.UserControls
{
    /// <summary>
    /// Interaction logic for Bug.xaml
    /// </summary>
    public partial class Bug : UserControl
    {
        public string currentProject = "";
        public ProjectDataBase.Bugs.Bug bugDetails;
        public Bug(ProjectDataBase.Bugs.Bug bug, string _currentProject)
        {
            InitializeComponent();
            currentProject = _currentProject;
            Title.Text = bug.name;
            Description.Text = bug.description;
            TimeSpent.Text = "Time Spent: " + bug.timeSpent;
            Priority.Text = "Priority: " + bug.priority;
            StepsToReproduce.Text = "Steps to reproduce: " + bug.stepsToReproduce;
            bugDetails = bug;
        }

        private void AdminProject_Loaded(object sender, RoutedEventArgs e)
        {
       
        }

        public delegate void Delete (object sender, RoutedEventArgs e, string reference);
        public event Delete _Delete;
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            _Delete(sender, e, bugDetails.reference);
        }

        private void DropDownBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DropDown1.Visibility == Visibility.Visible)
            {
                DropDownText.Text= "More";
                DropDownSymbol.Text = " ▼";
                DropDown1.Visibility = Visibility.Collapsed;
                DropDown2.Visibility = Visibility.Collapsed;
            }
            else
            {
                DropDownText.Text = "Less";
                DropDownSymbol.Text = " ▲";
                DropDown1.Visibility = Visibility.Visible;
                DropDown2.Visibility = Visibility.Visible;
            }
        }

        public void SetUserType(string type)
        {
            if (type == "user")
            {
                EditBugBtn.Visibility = Visibility.Collapsed;
                DeleteBtn.Visibility = Visibility.Collapsed;
            }
            else if (type == "dev")
            {
                EditBugBtn.Visibility = Visibility.Visible;
                DeleteBtn.Visibility = Visibility.Visible;
            }
            else if (type == "admin")
            {
                EditBugBtn.Visibility = Visibility.Visible;
                DeleteBtn.Visibility = Visibility.Visible;
            }
        }

        private void Bug_Loaded(object sender, RoutedEventArgs e)
        {

        }
        public void InitializeBug(ProjectDataBase.Bugs.Bug bug)
        {
            bugDetails = bug;
            Title.Text = bug.name;
            Description.Text = bug.description;
            TimeSpent.Text = "Time Spent: " + bug.timeSpent;
            Priority.Text = "Priority: " + bug.priority;
            StepsToReproduce.Text = "Steps to reproduce: " + bug.stepsToReproduce;

        }
        public void SetBugData(ProjectDataBase.Bugs.Bug bug)
        {

        }

        private void EditBugBtn_Click(object sender, RoutedEventArgs e)
        {
            Edit.Visibility = Visibility.Visible;
            EditBug.currentProject = currentProject;
            EditBug.Reference = bugDetails.reference;
            EditBug.BugName.Text = bugDetails.name;
            EditBug.BugDescription.Text = bugDetails.description;
            EditBug.BugPriority.Text = bugDetails.priority;
            EditBug.BugTimeSpent.Text = bugDetails.timeSpent;
            EditBug.BugStepsToReproduce.Text = bugDetails.stepsToReproduce;
            MainPanel.Visibility = Visibility.Collapsed;
        }

        private void EditBug__SaveBugBtn(object sender, RoutedEventArgs e, ProjectDataBase.Bugs.Bug bug, string projectName)
        {
            Edit.Visibility = Visibility.Collapsed;
            MainPanel.Visibility = Visibility.Visible;
            ProjectDataBase.Bugs.UpdateBug(projectName, bugDetails.reference, bug);
            InitializeBug(bug);
        }
    }
}
