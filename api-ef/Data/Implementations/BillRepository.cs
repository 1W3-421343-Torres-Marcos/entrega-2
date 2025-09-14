using api_ef.Data.Models;
using api_ef.Data.Repositories;

namespace api_ef.Data.Implementations
{
    public class BillRepository : IBillRepository
    {
        private ComercioDBContext _dbContext;
        public BillRepository(ComercioDBContext context)
        {
            _dbContext = context;
        }
        public void Create(Factura bill)
        {
            _dbContext.Facturas.Add(bill);
            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var factura = GetById(id);
            if (factura != null)
            {
                _dbContext.Remove(factura);
                _dbContext.SaveChanges();
            }
        }

        public List<Factura> GetAll()
        {
            return _dbContext.Facturas.ToList();
        }

        public Factura? GetById(int id)
        {
            return _dbContext.Facturas.Find(id);
        }

        public void Update(Factura bill)
        {
            if (bill != null)
            {
                _dbContext.Facturas.Update(bill);
                _dbContext.SaveChanges();
            }
        }
    }
}
