using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TestFilForValidation.Data
{
    public class EmployeeService : IEmployeeService
    {
        string jsonFilePath = @"C:\Users\herman.kittilsen\source\repos\TestFilForValidation\TestFilForValidation\Data\employees.Json";
        private List<Employee> employees;

        public EmployeeService()
        {
            LoadEmployeesFromJsonFile();
        }

        private void LoadEmployeesFromJsonFile()
        {
            

            try
            {
                string? jsonContent = File.ReadAllText(jsonFilePath);
                Data? data = JsonSerializer.Deserialize<Data>(jsonContent);
                employees = data.Employees;
                Console.WriteLine(employees.Count + " employees loaded");
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it or provide a default list of employees
                employees = new List<Employee>();
            }
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return employees.FirstOrDefault(e => e.EmployeeID == id);
        }

        public void DeleteEmployee(int employeeId)
        {
            try
            {
                Employee? EmployeeToDelete = employees.FirstOrDefault(e => e.EmployeeID == employeeId);

                if (EmployeeToDelete != null)
                {
                    employees.Remove(EmployeeToDelete);

                    string updatedJson = JsonSerializer.Serialize(new Data { Employees = employees }, new JsonSerializerOptions { WriteIndented = true });

                    File.WriteAllText(jsonFilePath, updatedJson);

                    Console.WriteLine($"Employee {EmployeeToDelete.Fullname} successfully deleted from the JSON file.");
                }
                else
                {
                    Console.WriteLine($"Employee with ID {employeeId} not found in the JSON file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting the employee from the JSON file: " + ex.Message);
            }
        }



    }
}