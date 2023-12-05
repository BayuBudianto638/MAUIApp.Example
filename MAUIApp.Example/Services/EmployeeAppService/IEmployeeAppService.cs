using MAUIApp.Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIApp.Example.Services.EmployeeAppService
{
    public interface IEmployeeAppService
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<(bool, string)> CreateEmployeeAsync(Employee employee);
        Task<(bool, string)> UpdateEmployeeAsync(Employee updatedEmployee);
        Task<(bool, string)> DeleteEmployeeAsync(int id);
    }
}
