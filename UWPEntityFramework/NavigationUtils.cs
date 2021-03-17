using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPEntityFramework.ADONet;
using UWPEntityFramework.EF;
using Windows.UI.Xaml.Controls;

namespace UWPEntityFramework
{
    static class NavigationUtils
    {
        static Dictionary<string, Type> pages = new Dictionary<string, Type>
        {
            { "Execute Scalar", typeof(ExecuteScalarPage) },
            { "Execute Reader", typeof(DataReaderPage) },
            { "Execute Non Query", typeof(ExecuteNonQueryPage)},
            { "Data Table", typeof(DataTablePage) },
            { "SQL Injection", typeof(SqlInjectionPage) },
            { "All Employees", typeof(AllEmployeesPage) },
            { "Employees Datagrid", typeof(EmployeesGridPage) },
            { "Get Employee by Id", typeof(GetEmployeeByIdPage) },
            { "New Employee", typeof(NewEmployeePage) },
            { "Get Employees by City", typeof(GetEmployeesByCityPage) }
        };

        public static void NavigateToPage(this Page page, NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            page.Frame.Navigate(pages[args.InvokedItem.ToString()]);
        }
    }
}
