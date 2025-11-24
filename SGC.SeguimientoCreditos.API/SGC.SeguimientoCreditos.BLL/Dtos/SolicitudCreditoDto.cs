namespace SGC.SeguimientoCreditos.BLL.Dtos
{
    public class SolicitudCreditoDto
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string IdentificacionCliente { get; set; } = null!;
        public decimal Monto { get; set; }
        public string? Comentarios { get; set; }
        public string? Estado { get; set; }
    }
}
