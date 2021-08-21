using MyNotes.Contracts.V1;

namespace MyNotes.Services.Helpers
{
    public static class ErrorHelper
    {
        public static BaseResponseDto ErrorResult(string message)
            => new BaseResponseDto { Result = false, Message = message };
    }
}
