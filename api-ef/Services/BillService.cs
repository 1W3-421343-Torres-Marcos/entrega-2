using api_ef.Data.Implementations;
using api_ef.Data.Models;
using api_ef.Data.Repositories;

namespace api_ef.Services
{
    public class BillService : IBillService
    {
        private readonly IBillRepository _billRepository;

        public BillService(IBillRepository repositorio)
        {
            _billRepository = repositorio;
        }
        public void DeleteBill(int id)
        {
            _billRepository.Delete(id);
        }

        public Factura? GetBill(int id)
        {
            return _billRepository.GetById(id);
        }

        public List<Factura> GetBills()
        {
            return _billRepository.GetAll();
        }

        public void SaveBill(Factura bill)
        {
            _billRepository.Create(bill);
        }

        public void UpdateBill(Factura bill)
        {
            _billRepository.Update(bill);
        }
    }
}
