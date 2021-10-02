using System.Collections.Generic;

namespace MyNotes.Contracts.V1.Response
{
    public class StartingPageDto : TopicDto
    {
        public List<ParagraphDto> Paragraphs { get; set; }
    }
}
