using Microsoft.Toolkit.Uwp.UI.Controls;
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
    public sealed partial class GetEmployeeByIdPage : Page
    {
        public GetEmployeeByIdPage()
        {
            this.InitializeComponent();
        }

        private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            this.NavigateToPage(sender, args);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var employeeIDBox = (TextBox)this.FindName("employeeID");
            var employeeNameBlock = (TextBlock)this.FindName("employeeName");
            int employeeId = int.Parse(employeeIDBox.Text);
            using (NorthwindContext context = new NorthwindContext())
            {
                employeeNameBlock.Text = (from emp in context.Employees
                                          where emp.EmployeeID == employeeId
                                          select $"{emp.FirstName} {emp.LastName}").SingleOrDefault();
            }
        }
    }
}
