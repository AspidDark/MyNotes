using System;
using System.ComponentModel.DataAnnotations;

namespace MyNotes.Contracts.V1.Request
{
    public class TopicCreateRequest
    {
        public string Name { get; set; }
    }
}
