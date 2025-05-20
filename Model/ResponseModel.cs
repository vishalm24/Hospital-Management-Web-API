namespace Hospital_Management.Model
{
    public class ResponseModel<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public void SetSeccess()
        {
            StatusCode = 200;
            Message = "Success";
        }
        public void SetSeccess(T data)
        {
            StatusCode = 200;
            Message = "Success";
            Data = data;
        }
        //public void SetFailure(string message)
        //{
        //    StatusCode = 417;
        //    Message = message;
        //}
        //public void AccessDenied(string message)
        //{
        //    StatusCode = 401;
        //    Message = message;
        //}
    }
}
