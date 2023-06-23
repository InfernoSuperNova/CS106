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

namespace LoginSignUp.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AdminProject : UserControl
    {

        public AdminProject()
        {
            InitializeComponent();
        }

        public delegate void DeleteProject(object sender, RoutedEventArgs e, string name);
        public event DeleteProject _DeleteProject;
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string name = ProjectTitle.Text;
            _DeleteProject(sender, e, name);
        }

        //Creating a public event handler
        public event EventHandler ToggleVisibilityClicked;

        //Function to handle click
        private void visabilityToggle_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("clicked");
            //here we are using a null operator once the click is activated it will try run code
            //when this happens the value is no longer null so it will execute functionality
            ToggleVisibilityClicked?.Invoke(this, e);
        }
    }
}
