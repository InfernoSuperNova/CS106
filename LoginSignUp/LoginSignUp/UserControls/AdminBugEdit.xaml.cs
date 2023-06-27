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
    /// Interaction logic for AdminBugEdit.xaml
    /// </summary>
    public partial class AdminBugEdit : UserControl
    {
        public string reference = "";
        public AdminBugEdit(string _reference)
        {
            reference = _reference;
            InitializeComponent();
        }

        private void AdminProject_Loaded(object sender, RoutedEventArgs e)
        {
       
        }

        public delegate void Delete (object sender, RoutedEventArgs e, string reference);
        public event Delete _Delete;
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            _Delete(sender, e, reference);
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
    }
}
