using AutoMapper;
using MyNotes.Contracts.V1.Response;
using MyNotes.Domain.Entities.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Services.Map
{
    public class InternalToResponse : Profile
    {
        public InternalToResponse()
        {
            CreateMap<Topic, TopicDto>();
        }
    }
}
