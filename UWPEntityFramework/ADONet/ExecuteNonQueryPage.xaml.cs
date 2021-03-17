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

namespace UWPEntityFramework.ADONet
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExecuteNonQueryPage : Page
    {
        DAL dal = new DAL();
        public ExecuteNonQueryPage()
        {
            this.InitializeComponent();
        }

        private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            this.NavigateToPage(sender, args);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var successTextBlock = (TextBlock)this.FindName("successText");
            successText.Visibility = Visibility.Collapsed;
            var firstNameBox = (TextBox)this.FindName("FirstName");
            var lastNameBox = (TextBox)this.FindName("LastName");
            var firstName = firstNameBox.Text;
            var lastName = lastNameBox.Text;
            dal.CreateNewEmployee(firstName, lastName);
            successText.Visibility = Visibility.Visible;
        }
    }
}
