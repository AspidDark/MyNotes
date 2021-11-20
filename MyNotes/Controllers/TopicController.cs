using Microsoft.AspNetCore.Mvc;
using MyNotes.Contracts.V1;
using MyNotes.Contracts.V1.Request;
using MyNotes.Contracts.V1.Request.Queries;
using System;
using System.Threading.Tasks;
using AutoMapper;
using MyNotes.Services.InternalDto;
using MyNotes.Extensions;
using MyNotes.Services.ServiceContracts;
using Microsoft.AspNetCore.Authorization;

namespace MyNotes.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TopicController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITopicLogic _topicLogic;
        public TopicController(IMapper mapper, ITopicLogic topicLogic)
        {
            _mapper = mapper;
            _topicLogic = topicLogic;
        }

        [HttpGet(ApiRoutes.TopicsRoute.Get)]
        public async Task<IActionResult> Get([FromQuery] EntityQuery entityByUserIdQuery)
        {
            //Validate here
            var entityByUserIdfilter = _mapper.Map<ByEntityFilter>(entityByUserIdQuery);
            entityByUserIdfilter.UserId = HttpContext.GetUserId();
            var response = await _topicLogic.Get(entityByUserIdfilter);
            return Ok(response);
        }

        [HttpGet(ApiRoutes.TopicsRoute.GetList)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery)
        {
            BaseUserIdFilter userIdFilter = new()
            { 
                UserId = HttpContext.GetUserId()
            };
            var paginationfilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var response = await _topicLogic.GetList(userIdFilter, paginationfilter);
            return Ok(response);
        }

        [HttpPost(ApiRoutes.TopicsRoute.Create)]
        public async Task<IActionResult> Create([FromBody] TopicCreateRequest request)
        {

            var topicCreate = _mapper.Map<TopicCreate>(request);
            topicCreate.UserId = HttpContext.GetUserId();
            var response = await _topicLogic.Create(topicCreate);
            return Ok(response);
        }

        [HttpPut(ApiRoutes.TopicsRoute.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid topicId, [FromBody] TopicUpdateRequest request)
        {
            var topicUpdate = _mapper.Map<TopicUpdate>(request);
            topicUpdate.UserId = HttpContext.GetUserId();
            topicUpdate.TopicId = topicId;
            var response = await _topicLogic.Update(topicUpdate);
            return Ok(response);
        }

        [HttpDelete(ApiRoutes.TopicsRoute.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid topicId)
        {
            var result= await _topicLogic.Delete(topicId, HttpContext.GetUserId());
            return Ok(result);
        }
    }
}
