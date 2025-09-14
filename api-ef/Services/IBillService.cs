using api_ef.Data.Models;

namespace api_ef.Services
{
    public interface IBillService
    {
            List<Factura> GetBills();
            void SaveBill(Factura bill);
            Factura? GetBill(int id);
            void DeleteBill(int id);
            void UpdateBill(Factura bill);
    }
}
