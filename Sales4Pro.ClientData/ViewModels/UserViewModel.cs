using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class UserViewModel : ObservableObject
{
    public UserViewModel()
    {
        UserId = Guid.NewGuid().ToString();
        Role = "User";
        Agents = new List<MetadataAgent>();
        Pricelists = new List<MetadataPricelist>();
        CustomerNumbers = new List<string>();
    }

    public override string ToString()
    {
        return UserName;
    }

    #region Observable Properties

    [ObservableProperty]
    public string userId;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string userName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string password;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string displayName;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string role;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public bool isPriceOnConfirmVisible;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public bool processOrders;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string defaultPricelistNumber;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string email;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public List<MetadataAgent> agents;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public List<MetadataPricelist> pricelists;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public List<string> customerNumbers;

    #endregion

    #region Computed Properties

    public bool ComputeIsAdmin
    {
        get { return Role.ToLower() == "admin"; }
    }

    public bool ComputeIsUser
    {
        get
        { return Role.ToLower() == "user"; }
    }

    public bool ComputeIsUserOrAdmin
    {
        get
        {
            return Role.ToLower() == "user" ||
                   Role.ToLower() == "admin";
        }
    }

    public bool ComputeIsB2B
    {
        get { return Role.ToLower() == "b2b"; }
    }

    public string ComputeDisplayName
    {
        get { return DisplayName; }
    }

    public string ComputeDisplayNameAndRole
    {
        get { return string.Format("{0} ({1})", DisplayName, Role); }
    }

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(UserName) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(DisplayName))
                return false;
            else
                return true;
        }
    }

    #endregion

    public void PasteData(User user)
    {
        if (user == null)
        {
            UserId = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;

            Role = string.Empty;
            DefaultPricelistNumber = string.Empty;
            DisplayName = string.Empty;
            IsPriceOnConfirmVisible = false;
            ProcessOrders = false;
            Email = string.Empty;

            Agents = new List<MetadataAgent>();
            Pricelists = new List<MetadataPricelist>();
            CustomerNumbers = new List<string>();
        }
        else
        {
            UserId = user.UserId;
            UserName = user.UserName;
            Password = user.Password;

            user.DeserializeMetadata();

            Role = user.MetadataContent.Role;
            DefaultPricelistNumber = user.MetadataContent.DefaultPricelistNumber;
            DisplayName = user.MetadataContent.DisplayName;
            IsPriceOnConfirmVisible = user.MetadataContent.IsPriceOnConfirmVisible;
            ProcessOrders = user.MetadataContent.ProcessOrders;
            Email = user.MetadataContent.Email;

            Agents = user.MetadataContent.Agents;
            Pricelists = user.MetadataContent.Pricelists;
            CustomerNumbers = user.MetadataContent.CustomerNumbers;
        }
        OnPropertyChanged(nameof(ComputeIsPrimaryButtonEnabled));
        OnPropertyChanged(nameof(ComputeIsAdmin));
        OnPropertyChanged(nameof(ComputeIsUser));
        OnPropertyChanged(nameof(ComputeIsUserOrAdmin));
        OnPropertyChanged(nameof(ComputeIsB2B));
        OnPropertyChanged(nameof(ComputeDisplayName));
        OnPropertyChanged(nameof(ComputeDisplayNameAndRole));
    }

    public User GetModel()
    {
        User model = new()
        {
            UserId = UserId,
            UserName = UserName,
            Password = Password,
            MetadataContent = new()
            {
                Role = Role,
                DefaultPricelistNumber = DefaultPricelistNumber,
                DisplayName = DisplayName,
                IsPriceOnConfirmVisible = IsPriceOnConfirmVisible,
                ProcessOrders = ProcessOrders,
                Email = Email,
                Agents = Agents,
                Pricelists = Pricelists,
                CustomerNumbers = CustomerNumbers
            }
        };
        model.SerializeMetadata();
        return model;
    }

}
