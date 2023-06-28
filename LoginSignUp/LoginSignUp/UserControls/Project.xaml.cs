using System;
using System.Collections.Generic;
using LoginSignUp.UserControls;
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
using LoginSignUp.classes;
namespace LoginSignUp.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Project : UserControl
    {
        private EmployeeDeletePrompt deletePrompt;
        public Project()
        {
            InitializeComponent();
            deletePrompt = new EmployeeDeletePrompt();
            deletePrompt._CancelDelete += CancelDelete;
            deletePrompt._ConfirmDelete += ConfirmDelete;
        }
        

        
      
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteField.Children.Add(deletePrompt);
        }
        private void CancelDelete(object sender, RoutedEventArgs e)
        {
            DeleteField.Children.Remove(deletePrompt);
        }

        public delegate void DeleteProject(object sender, RoutedEventArgs e, string name);
        public event DeleteProject _DeleteProject;
        private void ConfirmDelete(object sender, RoutedEventArgs e)
        {
            string name = ProjectTitle.Text;
            _DeleteProject(sender, e, name);
        }

        public delegate void EditBugMenu(object sender, RoutedEventArgs e, string projectName);
        public event EditBugMenu _EditBugMenu;
        private void EditBugButton_Click(object sender, RoutedEventArgs e)

        {
            _EditBugMenu(sender, e, ProjectTitle.Text);
        }
                //Creating a public event handler


        public delegate void AddBugProject(object sender, RoutedEventArgs e, string projectName);
        public event AddBugProject _AddBugProject;

        private void AddBugBtn_Click(object sender, RoutedEventArgs e)
        {
            _AddBugProject(sender, e, ProjectTitle.Text);
        }

        public delegate void ToggleEmployees(object sender, RoutedEventArgs e, string projectName);
        public event ToggleEmployees _ToggleEmployees;
        private void ToggleEmployeesVisibility_Click(object sender, RoutedEventArgs e)
        {
            _ToggleEmployees(sender, e, ProjectTitle.Text);
        }


        //public event EventHandler ToggleVisibilityClicked;

        ////Function to handle click
        //private void visabilityToggle_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("clicked");
        //    //here we are using a null operator once the click is activated it will try run code
        //    //when this happens the value is no longer null so it will execute functionality
        //    ToggleVisibilityClicked?.Invoke(this, e);
        //}
        public void SetUserType(string type)
        {
            if (type == "user")
            {
                AddBugBtn.Visibility = Visibility.Visible;
                EditBugButton.Visibility = Visibility.Visible;
                EditText.Text = "View Bugs";
                Delete.Visibility = Visibility.Hidden;
                ToggleEmployeesVisibility.Visibility = Visibility.Hidden;
                CreateBugReport.Visibility = Visibility.Hidden;
            }
            else if (type == "dev")
            {
                AddBugBtn.Visibility = Visibility.Visible;
                EditBugButton.Visibility = Visibility.Visible;
                EditText.Text = "Edit Bugs";
                Delete.Visibility = Visibility.Hidden;
                ToggleEmployeesVisibility.Visibility = Visibility.Hidden;
                CreateBugReport.Visibility = Visibility.Hidden;
            }
            else if (type == "admin")
            {
                AddBugBtn.Visibility = Visibility.Visible;
                EditBugButton.Visibility = Visibility.Visible;
                EditText.Text = "Edit Bugs";
                Delete.Visibility = Visibility.Visible;
                ToggleEmployeesVisibility.Visibility = Visibility.Visible;
                CreateBugReport.Visibility = Visibility.Visible;
            }
        }
    }
}
