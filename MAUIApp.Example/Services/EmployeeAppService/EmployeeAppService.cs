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

namespace MAUIApp.Example.Services.EmployeeAppService
{
    public class EmployeeAppService: IEmployeeAppService
    {
        private readonly HttpClient _httpClient;
        private string _apiUrl;

        public EmployeeAppService()
        {
            _httpClient = new HttpClient();
        }

        public void SetApiUrl(string apiUrl)
        {
            _apiUrl = apiUrl;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            var response = await _httpClient.GetAsync(_apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var employees = await response.Content.ReadFromJsonAsync<IEnumerable<Employee>>();
                return employees;
            }

            return null;
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiUrl}/{id}");

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
                var response = await _httpClient.PostAsync(_apiUrl, stringContent);

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
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(updatedEmployee), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_apiUrl}/{id}", stringContent);

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
                var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");

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
