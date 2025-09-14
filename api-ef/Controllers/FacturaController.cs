using api_ef.Data.Models;
using api_ef.Dtos;
using api_ef.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_ef.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : Controller
    {
        public IBillService _billService;
        public FacturaController(IBillService service)
        {
            _billService = service;
        }

        [HttpGet]
        public ActionResult GetFacturas()
        {
            try 
            {
                return Ok(_billService.GetBills());
            }
            catch
            {
                return StatusCode(500, "error interno");
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetFacturaById(int id)
        {
            try
            {
                if (_billService.GetBill(id) != null)
                {
                    return Ok(_billService.GetBill(id));
                }
                return StatusCode(404, "no hay");
            }
            catch
            {
                return StatusCode(500, "error interno");
            }
        }

        [HttpPost]
        public ActionResult Create([FromBody]FacturaDto factura)
        {
            try
            {
                _billService.SaveBill(factura);
                return StatusCode(201, "exito");
            }
            catch
            {
                return StatusCode(500, "error interno");
            }
        }

        [HttpPut]
        public ActionResult Update([FromBody]FacturaDto factura)
        {
            try
            {
                _billService.UpdateBill(factura);
                return StatusCode(201, "actualizado con exito");
            }
            catch
            {
                return StatusCode(500, "error interno");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _billService.DeleteBill(id);
                return StatusCode(200, "eliminado con exito");
            }
            catch
            {
                return StatusCode(500, "error interno");
            }
        }
    }
}
