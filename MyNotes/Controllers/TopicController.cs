using Microsoft.AspNetCore.Mvc;
using MyNotes.Contracts.V1;
using MyNotes.Contracts.V1.Request;
using MyNotes.Contracts.V1.Request.Queries;
using System;
using System.Threading.Tasks;
using AutoMapper;
using MyNotes.Services.InternalDto;

namespace MyNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly IMapper _mapper;
        public TopicController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet(ApiRoutes.Topics.Get)]
        public async Task<IActionResult> Get([FromQuery] EntityByUserIdQuery entityByUserIdQuery)
        {
            var entityByUserIdfilter = _mapper.Map<EntityByUserIdFilter>(entityByUserIdQuery);
            return Ok();
        }

        [HttpGet(ApiRoutes.Topics.GetAll)]
        public async Task<IActionResult> GetAll([FromQuery] BaseUserIdQuery query, [FromQuery] PaginationQuery paginationQuery)
        {
            var userIdFilter = _mapper.Map<BaseUserIdFilter>(query);
            var paginationfilter = _mapper.Map<PaginationFilter>(paginationQuery);
            return Ok();
        }

        [HttpPost(ApiRoutes.Topics.Create)]
        public async Task<IActionResult> Create([FromBody] TopicCreateRequest createTopicRequest)
        {
            return Ok();
        }

        [HttpPut(ApiRoutes.Topics.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid topicId, [FromBody] TopicUpdateRequest request)
        {

            return Ok();
        }

        [HttpDelete(ApiRoutes.Topics.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid topicId, [FromRoute] Guid userId)
        {
            return Ok();
        }
    }
}
