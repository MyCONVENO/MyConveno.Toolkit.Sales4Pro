namespace MyConveno.Toolkit.Sales4Pro.Client.ClientData;

public interface IPaymentTermAccessService
{
    Task<bool> AddPaymentTerm(PaymentTerm paymentTerm);
    Task<bool> DeletePaymentTerm(string paymentTermid);
    Task<bool> DeleteAllPaymentTerms();
    Task<List<PaymentTerm>> GetAllPaymentTermsAsync();
    void Initialize();
    Task<bool> UpdatePaymentTerm(PaymentTerm paymentTerm);
}