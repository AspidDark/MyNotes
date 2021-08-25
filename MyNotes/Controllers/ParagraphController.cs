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

namespace MyNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParagraphController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IParagraphLogic _paragraphLogic;

        public ParagraphController(IMapper mapper, IParagraphLogic paragraphLogic)
        {
            _mapper = mapper;
            _paragraphLogic = paragraphLogic;
        }

        [HttpGet(ApiRoutes.ParagraphRoute.Get)]
        public async Task<IActionResult> Get([FromQuery] EntityQuery entityByUserIdQuery)
        {
            //Validate here
            var entityByUserIdfilter = _mapper.Map<ByEntityFilter>(entityByUserIdQuery);
            entityByUserIdfilter.UserId = HttpContext.GetUserId();
            var response = await _paragraphLogic.Get(entityByUserIdfilter);
            return Ok(response);
        }

        [HttpGet(ApiRoutes.ParagraphRoute.GetList)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] Guid topicId)
        {
            ByEntityFilter userIdFilter = new()
            {
                UserId = HttpContext.GetUserId(),
                EntityId = topicId
            };
            var paginationfilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var response = await _paragraphLogic.GetList(userIdFilter, paginationfilter);
            return Ok(response);
        }

        [HttpPost(ApiRoutes.ParagraphRoute.Create)]
        public async Task<IActionResult> Create([FromBody] ParagraphCreateRequest request)
        {
            var topicCreate = _mapper.Map<ParagraphCreate>(request);
            topicCreate.UserId = HttpContext.GetUserId();
            var response = await _paragraphLogic.Create(topicCreate);
            return Ok(response);
        }

        [HttpPut(ApiRoutes.ParagraphRoute.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid paragraphId, [FromBody] ParagraphUpdateRequest request)
        {
            var topicUpdate = _mapper.Map<ParagraphUpdate>(request);
            topicUpdate.UserId = HttpContext.GetUserId();
            topicUpdate.ParagraphId = paragraphId;
            var response = await _paragraphLogic.Update(topicUpdate);
            return Ok(response);
        }

        [HttpDelete(ApiRoutes.ParagraphRoute.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid paragraphId)
        {
            var result = await _paragraphLogic.Delete(paragraphId, HttpContext.GetUserId());
            return Ok(result);
        }
    }
}
