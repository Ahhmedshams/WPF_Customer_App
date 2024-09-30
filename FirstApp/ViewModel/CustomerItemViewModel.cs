using FirstApp.Model;

namespace FirstApp.ViewModel;

public class CustomerItemViewModel(Customer model) : ValidationViewModelBase
{
    private readonly Customer _model = model;

    public int Id => _model.Id;

    public string? FirstName
    {
        get => _model.FirstName;
        set
        {
            _model.FirstName = value;
            RaisePropertyChanged();

            if (string.IsNullOrWhiteSpace(value))
            {
                AddError("First Name is Required");
            }
            else
            {
                ClearErrors();
            }
        }
    }

    public string? LastName
    {
        get => _model.LastName;
        set
        {
            _model.LastName = value;
            RaisePropertyChanged();
        }
    }

    public bool IsDeveloper
    {
        get => _model.IsDeveloper;
        set
        {
            _model.IsDeveloper = value;
            RaisePropertyChanged();
        }
    }
}

