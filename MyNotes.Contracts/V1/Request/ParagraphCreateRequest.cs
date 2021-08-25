using System;

namespace MyNotes.Contracts.V1.Request
{
    public class ParagraphCreateRequest
    {
        public string Name { get; set; }
        public Guid TopicId { get; set; }
    }
}
