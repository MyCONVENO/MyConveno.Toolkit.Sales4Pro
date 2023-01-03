namespace MyConveno.Toolkit.Sales4Pro.Client.BaseDataProductImageUpdate;

public class BaseDataImageUpdateProgressItem
{
    public delegate void UpdateProgressChangedEventHandler(IEnumerable<BaseDataImageUpdateProgressItem> Result);

    internal BaseDataImageUpdateProgressItem()
    { }

    internal BaseDataImageUpdateProgressItem(Type t)
    {
        TableName = t.Name;
    }

    public string TableName { get; set; }
    public int TotalChanges { get; set; }
    public List<string> ImagePath { get; set; }
}
