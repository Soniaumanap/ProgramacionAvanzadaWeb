namespace SGC.SeguimientoCreditos.BLL.Utilidades
{
    public class CustomResponse<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public T Datos { get; set; }
    }
}
