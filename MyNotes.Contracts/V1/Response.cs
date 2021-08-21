namespace MyNotes.Contracts.V1
{
    public class Response<T> : BaseResponseDto
    {
        public Response() { }

        public Response(T response)
        {
            Data = response;
        }

        public T Data { get; set; }
    }
}
