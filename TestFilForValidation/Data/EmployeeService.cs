using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestFilForValidation.Data
{
    public class EmployeeService : IEmployeeService
    {
        readonly string jsonFilePath = @"C:\Users\herman.kittilsen\source\repos\TestFilForValidation\TestFilForValidation\Data\employees.Json";
        private List<Employee>? employees;

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

                if (data != null)
                {
                    employees = data.Employees;
                    Console.WriteLine(employees?.Count + " employees loaded");
                }
                else
                {
                    Console.WriteLine("Deserialization failed. Data is null.");
                    employees = new List<Employee>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an exception while trying to load the employees: " + ex.Message);
                employees = new List<Employee>();
            }

        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            if (employees != null)
            {
                var employee = employees.FirstOrDefault(e => e.EmployeeID == id);
                return await Task.FromResult(employee);
            }
            return null;
        }


        public async Task DeleteEmployee(int employeeId)
        {
            try
            {
                if (employees != null)
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
                else
                {
                    Console.WriteLine("Employees list is null."); // Handle the case when employees is null
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting the employee from the JSON file: " + ex.Message);
            }

            await Task.CompletedTask; // Simulate the completion of an asynchronous operation
        }
    }
}
