using api_ef.Data.Models;

namespace api_ef.Data.Repositories
{
    public interface IBillRepository
    {
        void Create(Factura bill);
        void Update(Factura bill);
        void Delete(int id);
        List<Factura> GetAll();
        Factura? GetById(int id);
    }
}
