namespace SGC.BLL
{
    public class CustomResponse<T>
    {
        public bool Ok { get; set; }
        public string? Mensaje { get; set; }
        public T? Dato { get; set; }

        public static CustomResponse<T> Success(T data, string? mensaje = null)
        {
            return new CustomResponse<T>
            {
                Ok = true,
                Mensaje = mensaje,
                Dato = data
            };
        }

        public static CustomResponse<T> Fail(string mensaje)
        {
            return new CustomResponse<T>
            {
                Ok = false,
                Mensaje = mensaje,
                Dato = default
            };
        }
    }
}
