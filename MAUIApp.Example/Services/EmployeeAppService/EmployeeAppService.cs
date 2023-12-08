using MAUIApp.Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using MAUIApp.Example.Services.Sessions;

namespace MAUIApp.Example.Services.EmployeeAppService
{
    public class EmployeeAppService : IEmployeeAppService
    {
        private readonly HttpClient _httpClient;

        public EmployeeAppService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var pageInfo = new PageInfo();
            string apiUrl = $"{AppSettings.ApiUrl}/api/Employee/GetAllEmployee?Page={pageInfo.Page}&PageSize={pageInfo.PageSize}&Skip={pageInfo.Size}";
            string token = SessionAppService.GetToken();

            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var employees = await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
                return employees;
            }

            return null;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{AppSettings.ApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadFromJsonAsync<Employee>();
                return employee;
            }

            return null;
        }

        public async Task<(bool, string)> CreateEmployeeAsync(Employee employee)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(AppSettings.ApiUrl, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Employee created successfully");
                }
                else
                {
                    return (false, $"Error creating employee: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Exception: {ex.Message}");
            }
        }

        public async Task<(bool, string)> UpdateEmployeeAsync(Employee updatedEmployee)
        {
            string id = string.Empty;
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(updatedEmployee), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{AppSettings.ApiUrl}/{id}", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Employee updated successfully");
                }
                else
                {
                    return (false, $"Error updating employee: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Exception: {ex.Message}");
            }
        }

        public async Task<(bool, string)> DeleteEmployeeAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{AppSettings.ApiUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return (true, "Employee deleted successfully");
                }
                else
                {
                    return (false, $"Error deleting employee: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                return (false, $"Exception: {ex.Message}");
            }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
