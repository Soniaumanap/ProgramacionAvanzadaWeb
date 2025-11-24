namespace SGC.SeguimientoCreditos.BLL.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Identificacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string? Email { get; set; }
    }
}
