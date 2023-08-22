using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TestFilForValidation.Data;

namespace TestFilForValidation.Data
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeById(int id);
    }
}
