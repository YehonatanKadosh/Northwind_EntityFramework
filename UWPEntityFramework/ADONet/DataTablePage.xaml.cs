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
    public sealed partial class DataTablePage : Page
    {
        DAL dal = new DAL();
        DataTable employeesTable;
        public DataTablePage()
        {
            this.InitializeComponent();
        }

        private void NavigationViewControl_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            this.NavigateToPage(sender, args);
        }

        private void FillDataGrid(DataTable table, DataGrid grid)
        {
            grid.Columns.Clear();
            grid.AutoGenerateColumns = false;
            for (int i = 0; i < table.Columns.Count; i++)
            {
                grid.Columns.Add(new DataGridTextColumn()
                {
                    Header = table.Columns[i].ColumnName,
                    Binding = new Binding { Path = new PropertyPath("[" + i.ToString() + "]") },
                    IsReadOnly = false
                });
            }

            var collection = new ObservableCollection<object>();
            foreach (DataRow row in table.Rows)
            {
                collection.Add(row.ItemArray);
            }

            grid.ItemsSource = collection;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            employeesTable = dal.LoadEmployees();
            DataGrid grid = (DataGrid)this.FindName("dataGrid");
            FillDataGrid(employeesTable, grid);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataGrid grid = (DataGrid)this.FindName("dataGrid");
            int rowNum = 0;
            foreach (var entry in grid.ItemsSource)
            {
                object[] itemArray = (object[]) entry;
                DataRow row = employeesTable.Rows[rowNum];
                row.ItemArray = itemArray;
                rowNum++;
            }
            dal.UpdateEmployees(employeesTable);
        }
    }
}
