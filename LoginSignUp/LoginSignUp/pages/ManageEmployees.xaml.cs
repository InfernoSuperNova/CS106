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
    public partial class ManageEmployees : Page
    {
        public ManageEmployees()
        {
            InitializeComponent();
        }


        private void PopulateEmployeeList()
        {
            UserField.Children.Clear();
            List<UserDatabase.User> users = UserDatabase.GetUsers();
            foreach (UserDatabase.User user in users)
            {
                ManagedEmployee employee = new ManagedEmployee();
                employee.EmployeeName.Text = user.name;
                employee.UserType.Text = user.type;
                employee.PasswordField.Text = user.password;

                string assignedProjects = "Assigned Projects: ";
                foreach (string project in user.projects)
                {
                    assignedProjects += project + ", ";
                }
                int end = assignedProjects.Length;
                assignedProjects.Remove(end - 2, 2);
                employee.AssignedProjects.Text = assignedProjects;
                UserField.Children.Add(employee);
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateEmployeeList();
        }

        public delegate void ReturnToMenu(object sender, RoutedEventArgs e);
        public event ReturnToMenu _ReturnToMenu;
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            _ReturnToMenu(sender, e);
        }
    }
}
