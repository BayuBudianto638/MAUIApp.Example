using MAUIApp.Example.Services.EmployeeAppService;
using MAUIApp.Example.ViewModels;

namespace MAUIApp.Example.Views.EmployeeViews;

public partial class EmployeeViewPage : ContentPage
{
    private readonly EmployeeViewModel _viewModel;
    private readonly EmployeeAppService employeeAppService;
    private readonly INavigation navigation;
    public EmployeeViewPage()
    {
        InitializeComponent();

        //_viewModel = new EmployeeViewModel(employeeAppService, navigation); 
        BindingContext = this;
    }

    public EmployeeViewPage(EmployeeViewModel viewModel): this()
    {
        //InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private async void OnAddClick(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EmployeeAddPage());
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadEmployeesAsync();
    }
}