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


namespace LoginSignUp.UserControls
{
    /// <summary>
    /// Interaction logic for EditAddedBugAdmin.xaml
    /// </summary>
    public partial class AddBug : UserControl
    {
        public string currentProject = "";
        public AddBug()
        {
            InitializeComponent();
        }
        public delegate void AddBugBtn(object sender, RoutedEventArgs e, ProjectDataBase.Bugs.Bug bug, string projectName);
        public event AddBugBtn _AddBugBtn;
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            ProjectDataBase.Bugs.Bug bug = new ProjectDataBase.Bugs.Bug();
            bug.name = BugName.Text;
            bug.priority = BugPriority.Text;
            bug.timeSpent = BugTimeSpent.Text;
            bug.description = BugDescription.Text;
            bug.stepsToReproduce = BugStepsToReproduce.Text;
            ClearFields();
            _AddBugBtn(sender, e, bug, currentProject);
            
        }

        //There's probably a better way to do this. But this is perfectly readable and performant, so it's good enough for me
        private void ClearFields()
        {
            BugName.Text = null;
            BugPriority.Text = null;
            BugTimeSpent.Text = null;
            BugDescription.Text = null;
            BugStepsToReproduce.Text = null;
        }

        internal void Enable(string projName)
        {
            Visibility = Visibility.Visible;
            currentProject = projName;
        }

        internal void Disable()
        {
            Visibility = Visibility.Hidden;
            currentProject = "";
        }

        private void BugName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(BugName.Text))
                BugNameWatermark.Visibility = Visibility.Visible;
            else
                BugNameWatermark.Visibility = Visibility.Hidden;
        }

        private void BugPriority_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(BugPriority.Text))
                BugPriorityWatermark.Visibility = Visibility.Visible;
            else
                BugPriorityWatermark.Visibility = Visibility.Hidden;
        }

        private void BugTimeSpent_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(BugTimeSpent.Text))
                BugTimeSpentWatermark.Visibility = Visibility.Visible;
            else
                BugTimeSpentWatermark.Visibility = Visibility.Hidden;
        }

        private void BugDescription_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(BugDescription.Text))
                BugDescriptionWatermark.Visibility = Visibility.Visible;
            else
                BugDescriptionWatermark.Visibility = Visibility.Hidden;
        }

        private void BugStepsToReproduce_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(BugStepsToReproduce.Text))
                BugStepsToReproduceWatermark.Visibility = Visibility.Visible;
            else
                BugStepsToReproduceWatermark.Visibility = Visibility.Hidden;
        }
    }
}
