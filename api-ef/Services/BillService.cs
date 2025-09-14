using api_ef.Data.Implementations;
using api_ef.Data.Models;
using api_ef.Data.Repositories;
using api_ef.Dtos;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

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

        public FacturaDto? GetBill(int id)
        {
            Factura? factura = _billRepository.GetById(id);
            if (factura != null)
            {
                return MapToDto(factura);
            }
            return null;
        }

        public List<FacturaDto>? GetBills()
        {
            var lista = _billRepository.GetAll();
            var listaDTO = new List<FacturaDto>();

            if (lista.Count > 0)
            {
                foreach (var item in lista)
                {
                    var dto = MapToDto(item);
                    if (dto != null)
                    {
                        listaDTO.Add(dto);
                    }
                }
                return listaDTO;
            }
            else
            {
                return null;
            }
        }

        public void SaveBill(FacturaDto bill)
        {
            Factura factura = MapForCreation(bill);
            _billRepository.Create(factura);
        }

        public void UpdateBill(Factura bill)
        {
            _billRepository.Update(bill);
        }

        public FacturaDto? MapToDto(Factura factura)
        {
            if (factura == null)
            {
                return null;
            }
            var facturaDto = new FacturaDto
            {
                NroFactura = factura.NroFactura,
                Fecha = factura.Fecha,
                Cliente = factura.Cliente,
                FacturaActiva = factura.FacturaActiva,
            };
            if (factura.IdFormaNavigation != null)
            {
                facturaDto.FormaDePago = new FormasDePagoDTO
                {
                    Id = factura.IdFormaNavigation.Id,
                    Nombre = factura.IdFormaNavigation.Nombre,
                    EstaActivo = factura.IdFormaNavigation.EstaActivo
                };
            }
            if (factura.DetallesFacturas != null)
            {
                facturaDto.DetallesFacturas = factura.DetallesFacturas
                    .Select(df => new DetallesFacturaDTO
                    {
                        IdDetalle = df.IdDetalle,
                        IdFactura = df.IdFactura,
                        Articulo = new ArticuloDTO
                        {
                            Id = df.IdArticuloNavigation.Id,
                            Nombre = df.IdArticuloNavigation.Nombre,
                            PrecioUnitario = df.IdArticuloNavigation.PrecioUnitario ?? 0
                        },
                        Cantidad = df.Cantidad,
                    }).ToList();
            }
            return facturaDto;
        }

        public Factura MapForCreation(FacturaDto facturaDto)
        {
            var factura = new Factura
            {
                Fecha = facturaDto.Fecha,
                Cliente = facturaDto.Cliente,
                FacturaActiva = facturaDto.FacturaActiva
            };
            if (facturaDto.FormaDePago != null)
            {
                factura.IdForma = facturaDto.FormaDePago.Id;
            }
            if (facturaDto.DetallesFacturas != null)
            {
                factura.DetallesFacturas = facturaDto.DetallesFacturas
                    .Select(dfDto => new DetallesFactura
                    {
                        IdArticulo = dfDto.Articulo?.Id ?? 0,
                        Cantidad = dfDto.Cantidad
                    }).ToList();
            }
            return factura;
        }

        public void MapForUpdate(FacturaDto facturaDto, Factura existingFactura)
        {
            existingFactura.Fecha = facturaDto.Fecha;
            existingFactura.Cliente = facturaDto.Cliente;
            existingFactura.FacturaActiva = facturaDto.FacturaActiva;

            if (facturaDto.FormaDePago != null)
            {
                existingFactura.IdForma = facturaDto.FormaDePago.Id;
            }
            if (facturaDto.DetallesFacturas != null)
            {
                var detallesDtoIds = facturaDto.DetallesFacturas.Select(d => d.IdDetalle).ToList();
                var detallesAEliminar = existingFactura.DetallesFacturas
                    .Where(d => !detallesDtoIds.Contains(d.IdDetalle))
                    .ToList();
                foreach (var detalle in detallesAEliminar)
                {
                    existingFactura.DetallesFacturas.Remove(detalle);
                }
                foreach (var dfDto in facturaDto.DetallesFacturas)
                {
                    var detalleExistente = existingFactura.DetallesFacturas
                        .FirstOrDefault(d => d.IdDetalle == dfDto.IdDetalle);

                    if (detalleExistente != null)
                    {
                        detalleExistente.Cantidad = dfDto.Cantidad;
                        detalleExistente.IdArticulo = dfDto.Articulo?.Id ?? 0;
                    }
                    else
                    {
                        existingFactura.DetallesFacturas.Add(new DetallesFactura
                        {
                            IdFactura = existingFactura.NroFactura,
                            IdArticulo = dfDto.Articulo?.Id ?? 0,
                            Cantidad = dfDto.Cantidad,
                        });
                    }
                }
            }
        }
    }
}
