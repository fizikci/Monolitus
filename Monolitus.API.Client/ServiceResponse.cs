namespace Monolitus.API.Client
{
    public class ServiceResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }

        public string SessionId { get; set; }
        public int ErrorType { get; set; }
    }
}
