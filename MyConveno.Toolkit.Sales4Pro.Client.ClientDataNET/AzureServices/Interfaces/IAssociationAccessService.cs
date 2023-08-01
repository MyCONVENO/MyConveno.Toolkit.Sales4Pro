namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IAssociationAccessService
{
    Task<bool> AddAssociation(Association association);
    Task<bool> DeleteAssociation(string associationid);
    Task<List<Association>> GetAllAssociationsAsync();
    void Initialize();
    Task<bool> UpdateAssociation(Association association);
}