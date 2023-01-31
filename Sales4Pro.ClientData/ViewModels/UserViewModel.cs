using CommunityToolkit.Mvvm.ComponentModel;

namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public partial class UserViewModel : ObservableObject
{
    public override string ToString()
    {
        return UserName;
    }

    #region Observable Properties

    [ObservableProperty]
    public string userId = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string userName = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsPrimaryButtonEnabled))]
    public string password = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(ComputeIsAdmin))]
    [NotifyPropertyChangedFor(nameof(ComputeIsUser))]
    [NotifyPropertyChangedFor(nameof(ComputeIsUserOrAdmin))]
    [NotifyPropertyChangedFor(nameof(ComputeIsB2B))]
    public MetadataUserContent metadataUserContent = new();

    #endregion

    #region Computed Properties

    public bool ComputeIsAdmin
    {
        get { return MetadataUserContent.Role.ToLower() == "admin"; }
    }

    public bool ComputeIsUser
    {
        get
        { return MetadataUserContent.Role.ToLower() == "user"; }
    }

    public bool ComputeIsUserOrAdmin
    {
        get
        {
            return MetadataUserContent.Role.ToLower() == "user" ||
                   MetadataUserContent.Role.ToLower() == "admin";
        }
    }

    public bool ComputeIsB2B
    {
        get { return MetadataUserContent.Role.ToLower() == "b2b"; }
    }

    public string ComputeDisplayname
    {
        get { return MetadataUserContent.Displayname; }
    }

    public string ComputeDisplaynameAndRole
    {
        get { return string.Format("{0} ({1})", MetadataUserContent.Displayname, MetadataUserContent.Role); }
    }

    public bool ComputeIsPrimaryButtonEnabled
    {
        get
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
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

            MetadataUserContent = new MetadataUserContent();
        }
        else
        {
            UserId = user.UserId;
            UserName = user.UserName;
            Password = user.Password;

            User tempUser = new() { Metadata = user.Metadata };
            tempUser.DeserializeMetadata();
            MetadataUserContent = tempUser.MetadataContent;
        }
        OnPropertyChanged(nameof(ComputeIsAdmin));
        OnPropertyChanged(nameof(ComputeIsUser));
        OnPropertyChanged(nameof(ComputeIsUserOrAdmin));
        OnPropertyChanged(nameof(ComputeIsB2B));
        OnPropertyChanged(nameof(ComputeDisplayname));
        OnPropertyChanged(nameof(ComputeDisplaynameAndRole));
    }

    public User GetModel()
    {
        User model = new()
        {
            UserId = userId,
            UserName = userName,
            Password = password,
            MetadataContent = MetadataUserContent
        };
        model.SerializeMetadata();
        return model;
    }

}
