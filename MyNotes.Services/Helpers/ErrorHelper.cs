using MyNotes.Contracts.V1;

namespace MyNotes.Services.Helpers
{
    public static class ErrorHelper
    {
        public static BaseResponse ErrorResult(string message)
            => new BaseResponse { Result = false, Message = message };
    }
}
