using FirstApp.Data;
using FirstApp.Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Navigation;

namespace FirstApp.ViewModel;

public class CustomersViewModel(ICustomerDataProvider customerDataProvider) : ViewModelBase
{
    private readonly ICustomerDataProvider _customerDataProvider = customerDataProvider;
    private CustomerItemViewModel _selectedCustomer;
    private NavigationSide _navigationSide;

    public ObservableCollection<CustomerItemViewModel> Customers { get; } = [];
    public CustomerItemViewModel SelectedCustomer
    {
        get => _selectedCustomer;
        set {
            _selectedCustomer = value;
            RaisePropertyChanged();
        }
    }
    public NavigationSide NavigationSide
    {
        get => _navigationSide;
        private set
        {
            _navigationSide = value;
            RaisePropertyChanged();
        }
    }

    public async Task LoadAsync()
    {
        if (Customers.Any())
        {
            return;
        }

        var customers = await _customerDataProvider.GetAllAsync();
        if (customers != null)
        {
            foreach (var customerItem in customers)
            {
                Customers.Add(new CustomerItemViewModel(customerItem));
            }
        }
    }

    public Task Add()
    {
        var newCustomer = new CustomerItemViewModel( new Customer (){ FirstName = "new" });
        Customers.Add(newCustomer);
        SelectedCustomer = newCustomer;
        return Task.CompletedTask;
    }

    internal void MoveNavigation()
    {
        NavigationSide = NavigationSide == NavigationSide.Left
        ? NavigationSide.Right
        : NavigationSide.Left;
    }

}
public enum NavigationSide
{
    Left,
    Right
}
