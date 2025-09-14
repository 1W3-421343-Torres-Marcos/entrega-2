namespace api_ef.Dtos
{
    public class DetallesFacturaDTO
    {
        public int IdDetalle { get; set; }
        public int IdFactura { get; set; }
        public ArticuloDTO? Articulo { get; set; }
        public int? Cantidad { get; set; }
    }
}
