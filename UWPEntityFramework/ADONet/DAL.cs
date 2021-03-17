
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPEntityFramework.ADONet
{
    public class DAL
    {
        string connectionString;

        public DAL()
        {
            connectionString = "Data Source=DESKTOP-SN2MF5L;Initial Catalog=Northwind;Integrated Security=True";
        }

        public DataTable LoadEmployees()
        {
            DataTable employeesTable = new DataTable();
            string selectContent = "SELECT * FROM Employees";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(selectContent, connection))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataAdapter.Fill(employeesTable);
                }
            }
            return employeesTable;
        }

        public string GetEmployeeNameByID(int employeeID)
        {
            string name;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Creates SqlCommand with parameters (@)
                SqlCommand cmd = new SqlCommand("select FirstName + ' ' + LastName Name from Employees where EmployeeID=@ID", conn);
                // Bind the parameter
                SqlParameter param = new SqlParameter("@ID", employeeID);
                cmd.Parameters.Add(param);

                // Execute the command
                name = cmd.ExecuteScalar().ToString();
            }
            return name;
        }

        public IEnumerable<string> GetEmployeesNames()
        {
            List<string> employeeNames = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                ///                                         0           1          2
                SqlCommand cmd = new SqlCommand("select EmployeeID, FirstName, LastName from Employees", conn);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string firstName = reader.IsDBNull(1) ? null : (string)reader["FirstName"];
                        int employeeID = (int)reader["EmployeeID"];
                        
                        string lastName = (string)reader["LastName"];
                        employeeNames.Add($"{employeeID}. {firstName} {lastName}");
                    }
                }
            }
            return employeeNames;
        }

        public void UpdateEmployees(DataTable employeesTable)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select EmployeeID, FirstName, LastName from Employees", conn);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                // Use SqlCommandBuilder to generate Insert/Update/Delete commands for sync DataTable with the data in the database
                SqlCommandBuilder cmdBld = new SqlCommandBuilder(dataAdapter);
                dataAdapter.DeleteCommand = cmdBld.GetDeleteCommand();
                dataAdapter.InsertCommand = cmdBld.GetInsertCommand();
                dataAdapter.UpdateCommand = cmdBld.GetUpdateCommand();
                // Sync the database
                dataAdapter.Update(employeesTable);
                conn.Close();
            }
        }

        public DataTable GetEmployeesByCity(string city)
        {
            DataTable employees = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                ////////////////////////// Sql injection //////////////////////////////////////
                SqlCommand cmd = new SqlCommand(@"select EmployeeID, FirstName, LastName, City 
                                                  from Employees
                                                  where city='" + city + "'", conn);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                dataAdapter.Fill(employees);
            }
            return employees;
        }

        public void CreateNewEmployee(string firstName, string lastName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"INSERT INTO Employees (FirstName, LastName) VALUES (@firstName, @lastName)", conn);
                var firstNameParam = new SqlParameter("@firstName", firstName);
                var lastNameParam = new SqlParameter("@lastName", lastName);
                cmd.Parameters.AddRange(new SqlParameter[] { firstNameParam, lastNameParam });
                cmd.ExecuteNonQuery();
            }
        }
    }
}
