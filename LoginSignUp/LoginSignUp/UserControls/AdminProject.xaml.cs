﻿using System;
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
using LoginSignUp.classes;
namespace LoginSignUp.UserControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class AdminProject : UserControl
    {
        private EmployeeDeletePrompt deletePrompt;
        public AdminProject()
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

        public delegate void ManageEmployees(object sender, RoutedEventArgs e);
        public event ManageEmployees _ManageEmployees;

        private void ManageEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeBtn.Visibility = Visibility.Visible;
            SelectOptionText.Visibility = Visibility.Collapsed;
        }
    }
}
