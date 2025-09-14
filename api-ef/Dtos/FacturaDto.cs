namespace api_ef.Dtos
{
    public class FacturaDto
    {

            public int NroFactura { get; set; }

            public DateOnly? Fecha { get; set; }

            public FormasDePagoDTO FormaDePago { get; set; }

            public string Cliente { get; set; }

            public bool? FacturaActiva { get; set; }
            public ICollection<DetallesFacturaDTO> DetallesFacturas { get; set; } = new List<DetallesFacturaDTO>();
    }
}
