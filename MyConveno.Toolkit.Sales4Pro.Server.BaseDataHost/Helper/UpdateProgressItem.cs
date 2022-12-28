namespace MyConveno.Toolkit.Sales4Pro.Server.BaseDataHost
{
    public class UpdateProgressItem
    {
        public UpdateProgressItem(string tableName, int totalChanges)
        {
            TableName = tableName;
            TotalChanges = totalChanges;
        }

        public string TableName { get; set; }
        public int TotalChanges { get; set; }
    }
}
