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
    public class NotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INoteLogic _noteLogic;

        public NotesController(IMapper mapper, INoteLogic noteLogic)
        {
            _mapper = mapper;
            _noteLogic = noteLogic;
        }

        [HttpGet(ApiRoutes.NotesRoute.Get)]
        public async Task<IActionResult> Get([FromQuery] MainEntityQuery query)
        {
            //Validate here
            var entityByUserIdfilter = _mapper.Map<ByMainEntityFilter>(query);
            entityByUserIdfilter.UserId = HttpContext.GetUserId();
            var response = await _noteLogic.Get(entityByUserIdfilter);
            return Ok(response);
        }

        [HttpGet(ApiRoutes.NotesRoute.GetList)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] Guid paragraphId)
        {
            ByEntityFilter userIdFilter = new()
            {
                UserId = HttpContext.GetUserId(),
                EntityId = paragraphId
            };
            var paginationfilter = _mapper.Map<PaginationFilter>(paginationQuery);
            var response = await _noteLogic.GetList(userIdFilter, paginationfilter);
            return Ok(response);
        }

        [HttpPost(ApiRoutes.NotesRoute.Create)]
        public async Task<IActionResult> Create([FromBody] NoteCreateRequest request)
        {
            var topicCreate = _mapper.Map<NoteCreate>(request);
            topicCreate.UserId = HttpContext.GetUserId();
            var response = await _noteLogic.Create(topicCreate);
            return Ok(response);
        }

        [HttpPut(ApiRoutes.NotesRoute.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid noteId, [FromBody] NoteUpdateRequest request)
        {
            var topicUpdate = _mapper.Map<NoteUpdate>(request);
            topicUpdate.UserId = HttpContext.GetUserId();
            topicUpdate.NoteId = noteId;
            var response = await _noteLogic.Update(topicUpdate);
            return Ok(response);
        }

        [HttpDelete(ApiRoutes.ParagraphRoute.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid noteId)
        {
            var result = await _noteLogic.Delete(noteId, HttpContext.GetUserId());
            return Ok(result);
        }
    }
}
