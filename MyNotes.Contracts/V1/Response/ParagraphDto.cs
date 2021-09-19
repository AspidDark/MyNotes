using System;

namespace MyNotes.Contracts.V1.Response
{
    public class ParagraphDto : BaseResponseDto
    {
        public string Name { get; set; }

        public Guid TopicId { get; set; }
    }
}
