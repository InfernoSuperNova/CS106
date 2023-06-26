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
            bug.name = bugName.Text;
            bug.priority = bugPriority.Text;
            bug.timeSpent = bugTimeSpent.Text;
            bug.description = bugDescription.Text;
            bug.stepsToReproduce = bugStepsToReproduce.Text;
            ClearFields();
            _AddBugBtn(sender, e, bug, currentProject);
            
        }

        //There's probably a better way to do this. But this is perfectly readable and performant, so it's good enough for me
        private void ClearFields()
        {
            bugName.Text = null;
            bugPriority.Text = null;
            bugTimeSpent.Text = null;
            bugDescription.Text = null;
            bugStepsToReproduce.Text = null;
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
    }
}
