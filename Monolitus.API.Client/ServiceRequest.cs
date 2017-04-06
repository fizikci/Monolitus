namespace Monolitus.API.Client
{
    public class ServiceRequest<T>
    {
        public string ApiKey { get; set; }
        public T Data { get; set; }

        public string ClientIP { get; set; }
        public string Client { get; set; }
        public string SessionId { get; set; }
    }
}