using FirstApp.Command;
using FirstApp.Data;
using FirstApp.Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Navigation;

namespace FirstApp.ViewModel;

public class CustomersViewModel : ValidationViewModelBase
{
    private readonly ICustomerDataProvider _customerDataProvider;
    private CustomerItemViewModel _selectedCustomer;
    private NavigationSide _navigationSide;


    public CustomersViewModel(ICustomerDataProvider customerDataProvider)
    {
        _customerDataProvider = customerDataProvider;
        AddCommand = new DelegateCommand(Add);
        MoveNavigationCommand = new DelegateCommand(MoveNavigation);
        DeleteCommand = new DelegateCommand(Delete,CanDelete);
    }

    public ObservableCollection<CustomerItemViewModel> Customers { get; } = [];
    public CustomerItemViewModel SelectedCustomer
    {
        get => _selectedCustomer;
        set {
            _selectedCustomer = value;
            RaisePropertyChanged();
            RaisePropertyChanged(nameof(IsSelectedCustomer));
            DeleteCommand.RaiseCanExecuteChanged();
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

    public DelegateCommand AddCommand { get; }
    public DelegateCommand MoveNavigationCommand { get; }
    public DelegateCommand DeleteCommand { get; }

    public bool IsSelectedCustomer { get => _selectedCustomer != null; } 
    public async override Task LoadAsync()
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

    private void Add(object? parameter)
    {
        var newCustomer = new CustomerItemViewModel( new Customer (){ FirstName = "new" });
        Customers.Add(newCustomer);
        SelectedCustomer = newCustomer;
    }
    private void Delete(object? parameter)
    {
       if(SelectedCustomer is not null)
        {
            Customers?.Remove(SelectedCustomer);
            SelectedCustomer = null;
        }

    }
    private bool CanDelete(object? parameter) => SelectedCustomer is not null;
    
    private void MoveNavigation(object? parameter)
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
