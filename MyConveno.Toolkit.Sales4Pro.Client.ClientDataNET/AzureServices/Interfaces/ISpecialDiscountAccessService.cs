namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface ISpecialDiscountAccessService
{
    Task<bool> AddSpecialDiscount(SpecialDiscount specialDiscount);
    Task<bool> DeleteSpecialDiscount(string specialDiscountid);
    Task<List<SpecialDiscount>> GetAllSpecialDiscountsAsync();
    void Initialize();
    Task<bool> UpdateSpecialDiscount(SpecialDiscount specialDiscount);
}