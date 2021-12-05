namespace ContactService.Models
{
    public class GenericResponse<T>
    {
        public string Message { get; set; }
        public bool Result { get; set; }
        public T Response { get; set; }


        public static GenericResponse<T> SuccessResponse(string message, T responseData)
        {
            return new GenericResponse<T>
            {
                Response = responseData,
                Result = true,
                Message = message
            };
        }
        public static GenericResponse<T> ErrorResponse(string message)
        {
            return new GenericResponse<T>
            {
                Result = false,
                Message = message
            };
        }

    }
}