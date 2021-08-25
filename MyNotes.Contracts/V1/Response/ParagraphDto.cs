using System;

namespace MyNotes.Contracts.V1.Response
{
    public class ParagraphDto
    {
        public string Name { get; set; }

        public Guid TopicId { get; set; }

        public Guid Id { get; set; }
    }
}
