namespace MyNotes.Contracts.V1
{
    public class Response<T> : BaseResponse
    {
        public Response() { }

        public Response(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
    }
}
