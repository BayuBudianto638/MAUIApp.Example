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
        Task<ListEmployee> GetEmployeesAsync();
        Task<EmployeeModel> GetEmployeeByIdAsync(int id);
        Task<(bool, string)> CreateEmployeeAsync(EmployeeModel employee);
        Task<(bool, string)> UpdateEmployeeAsync(EmployeeModel updatedEmployee);
        Task<(bool, string)> DeleteEmployeeAsync(int id);
    }
}
