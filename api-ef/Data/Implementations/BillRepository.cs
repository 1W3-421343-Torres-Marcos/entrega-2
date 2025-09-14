using api_ef.Data.Models;
using api_ef.Data.Repositories;
using Microsoft.EntityFrameworkCore;

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
            var factura = _dbContext.Facturas.Find(id);
            if (factura != null)
            {
                factura.FacturaActiva = false;
                _dbContext.SaveChanges();
            }
        }

        public List<Factura> GetAll()
        {
            return _dbContext.Facturas
                             .Include(fp => fp.IdFormaNavigation)
                             .Include(f => f.DetallesFacturas)
                             .ThenInclude(df => df.IdArticuloNavigation)
                             .ToList();
        }

        public Factura? GetById(int id)
        {
            return _dbContext.Facturas
                             .Include(fp => fp.IdFormaNavigation)
                             .Include(f => f.DetallesFacturas)
                             .ThenInclude(df => df.IdArticuloNavigation)
                             .FirstOrDefault(f => f.NroFactura == id);
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
