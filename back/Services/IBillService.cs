using entrega_viernes_5_09.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entrega_viernes_5_09.Services
{
    public interface IBillService
    {
        List<Bill> GetBills();
        bool SaveBill(Bill bill);
        Bill? GetBill(int id);
        bool DeleteBill(int id);
        bool UpdateBill(Bill bill);
    }
}
