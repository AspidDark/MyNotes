using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Contracts.V1.Response
{
    public class FileDataResponseDto
    {
        public Guid ParagraphId { get; set; }

        public string Explanation { get; set; }

        public Guid Id { get; set; }
    }
}
