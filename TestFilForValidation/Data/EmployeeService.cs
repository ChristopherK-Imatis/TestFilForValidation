using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace TestFilForValidation.Data
{
    public class EmployeeService : IEmployeeService
    {
        private List<Employee> employees;

        public EmployeeService()
        {
            LoadEmployeesFromJsonFile();
        }

        private void LoadEmployeesFromJsonFile()
        {
            string jsonFilePath = @"C:\Users\herman.kittilsen\source\repos\TestFilForValidation\TestFilForValidation\Data\employees.Json";

            try
            {
                string jsonContent = File.ReadAllText(jsonFilePath);
                Data data = JsonSerializer.Deserialize<Data>(jsonContent);
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

        
    }
}