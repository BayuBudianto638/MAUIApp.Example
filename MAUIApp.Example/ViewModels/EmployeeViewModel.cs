﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIApp.Example.Models;
using MAUIApp.Example.Services.EmployeeAppService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MAUIApp.Example.ViewModels
{
    public partial class EmployeeViewModel : ObservableObject
    {
        private readonly EmployeeAppService _employeeService;
        private ObservableCollection<Employee> _employees;

        public event PropertyChangedEventHandler PropertyChanged;

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        public ObservableCollection<Employee> Employees
        {
            get => _employees;
            set
            {
                if (_employees != value)
                {
                    _employees = value;
                    OnPropertyChanged(nameof(Employees));
                }
            }
        }

        public ICommand LoadEmployeesCommand { get; }
        public ICommand CreateEmployeeCommand { get; }
        public ICommand UpdateEmployeeCommand { get; }
        public ICommand DeleteEmployeeCommand { get; }

        public EmployeeViewModel()
        {
            _employeeService = new EmployeeAppService();
            Employees = new ObservableCollection<Employee>();

            CreateEmployeeCommand = new Command<Employee>(async (employee) => await CreateEmployee(employee));
            UpdateEmployeeCommand = new Command<Employee>(async (employee) => await UpdateEmployee(employee));
            DeleteEmployeeCommand = new Command<int>(async (id) => await DeleteEmployee(id));

            LoadEmployeesCommand.Execute(null);
        }

        public async Task LoadEmployees()
        {
            await ExecuteAsync(async () =>
            {
                var costumers = await _employeeService.GetEmployeesAsync();
                if (costumers is not null && costumers.Any())
                {
                    Employees ??= new ObservableCollection<Employee>();

                    foreach (var costumer in costumers)
                    {
                        Employees.Add(costumer);
                    }
                }
            }, "Fetching costumers...");
        }

        private async Task CreateEmployee(Employee employee)
        {
            var (isCreated, isSuccess) = await _employeeService.CreateEmployeeAsync(employee);
            if (isCreated)
            {
                Employees.Add(employee);
            }
        }

        private async Task UpdateEmployee(Employee updatedEmployee)
        {
            var (isUpdated, isSuccess) = await _employeeService.UpdateEmployeeAsync(updatedEmployee);
            if (isUpdated)
            {
                var existingEmployee = Employees.FirstOrDefault(e => e.Id == updatedEmployee.Id);
                if (existingEmployee != null)
                {
                    existingEmployee.Code = updatedEmployee.Code;
                    existingEmployee.Name = updatedEmployee.Name;
                    existingEmployee.Age = updatedEmployee.Age;
                }
            }
        }

        private async Task DeleteEmployee(int id)
        {
            var (isDeleted, isSuccess) = await _employeeService.DeleteEmployeeAsync(id);
            if (isDeleted)
            {
                var deletedEmployee = Employees.FirstOrDefault(e => e.Id == id);
                if (deletedEmployee != null)
                {
                    Employees.Remove(deletedEmployee);
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async Task ExecuteAsync(Func<Task> operation, string? busyText = null)
        {
            IsBusy = true;
            BusyText = busyText ?? "Processing...";
            try
            {
                await operation?.Invoke();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
                BusyText = "Processing...";
            }
        }
    }
}
