namespace SGC.BLL.Dtos
{
    public class CustomResponse<T>
    {
        public bool EsError { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public T? Data { get; set; }
        public static CustomResponse<T> Ok(T data, string msg = "") => new() { Data = data, Mensaje = msg };
        public static CustomResponse<T> Fail(string msg) => new() { EsError = true, Mensaje = msg };
    }
}
