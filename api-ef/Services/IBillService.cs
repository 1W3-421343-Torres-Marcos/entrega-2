using api_ef.Data.Models;
using api_ef.Dtos;

namespace api_ef.Services
{
    public interface IBillService
    {
            List<FacturaDto>? GetBills();
            void SaveBill(FacturaDto bill);
            FacturaDto? GetBill(int id);
            void DeleteBill(int id);
            void UpdateBill(Factura bill);
    }
}
