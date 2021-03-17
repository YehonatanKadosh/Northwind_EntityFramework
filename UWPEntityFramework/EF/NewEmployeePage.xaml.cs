﻿using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPEntityFramework.EF
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewEmployeePage : Page
    {
        public NewEmployeePage()
        {
            this.InitializeComponent();
        }

        private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            this.NavigateToPage(sender, args);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            successText.Visibility = Visibility.Collapsed;
            var firstName = FirstName.Text;
            var lastName = LastName.Text;
            Employee newEmployee = new Employee
            {
                FirstName = firstName,
                LastName = lastName
            };
            using (NorthwindContext context = new NorthwindContext())
            {
                context.Employees.Add(newEmployee);
                context.SaveChanges();
            }
            successText.Visibility = Visibility.Visible;
            successText.Text = $"New employee ID is: {newEmployee.EmployeeID}";
        }
    }
}
