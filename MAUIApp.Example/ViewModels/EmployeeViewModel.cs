using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIApp.Example.Models;
using MAUIApp.Example.Services.EmployeeAppService;
using MAUIApp.Example.Views.EmployeeViews;
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
        private readonly EmployeeAppService _employeeAppService;
        private readonly INavigation _navigation;

        public EmployeeViewModel(EmployeeAppService employeeAppService, INavigation navigation)
        {
            _employeeAppService = employeeAppService;
            _navigation = navigation;
        }

        //public ICommand SetOperatingEmployeeCommand => new Command<EmployeeModel>(async (employee) =>
        //{
        //    await _navigation.PushAsync(new EmployeeAddPage());
        //});

        [ObservableProperty]
        private ObservableCollection<EmployeeModel> _employees = new();

        [ObservableProperty]
        private EmployeeModel _operatingEmployee = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private string _busyText;

        //public EmployeeModel OperatingEmployee { get; private set; }

        public async Task LoadEmployeesAsync()
        {
            var empAppService = new EmployeeAppService();

            await ExecuteAsync(async () => 
            {
                var employees = await empAppService.GetEmployeesAsync();

                if (employees is not null && (employees.data != null))
                {
                    Employees ??= new ObservableCollection<EmployeeModel>();
                    Employees.Clear();

                    foreach (var employee in employees.data)
                    {
                        Employees.Add(employee);
                    }
                }
            }, "Fetching employees...");
        }

        [RelayCommand]
        private void SetOperatingEmployee(EmployeeModel? employee) => OperatingEmployee = employee ?? new();

        [RelayCommand]
        private async Task SaveEmployeeAsync()
        {
            if (OperatingEmployee is null)
                return;

            var busyText = OperatingEmployee.Id == 0 ? "Creating Employee..." : "Updating Employee...";
            await ExecuteAsync(async () =>
            {
                if (OperatingEmployee.Id == 0)
                {
                    var (isCreate, isMsg) = await _employeeAppService.CreateEmployeeAsync(OperatingEmployee);
                }
                else
                {
                    var (isUpdate, Message) = await _employeeAppService.UpdateEmployeeAsync(OperatingEmployee);
                    if (isUpdate)
                    {
                        await Shell.Current.DisplayAlert("Success", "Employee creation success", "Ok");
                        return;
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Employee updation error", "Ok");
                        return;
                    }
                }
                SetOperatingEmployeeCommand.Execute(new());
            }, busyText);
        }

        [RelayCommand]
        private async Task DeleteEmployeeAsync(int id)
        {
            bool answer = await Shell.Current.DisplayAlert("Question?", "Do you want to delete this record?", "Yes", "No");

            if (answer.Equals(true))
            {
                await ExecuteAsync(async () =>
                {
                    var (isDeleted, Message) = await _employeeAppService.DeleteEmployeeAsync(id);
                    if (isDeleted)
                    {
                        await Shell.Current.DisplayAlert("Delete Success", "Employee was deleted", "Ok");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Delete Error", "Employee was not deleted", "Ok");
                    }
                }, "Deleting Employee...");
            }

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
