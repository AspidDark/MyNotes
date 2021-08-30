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
    public class FileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFileLogic _fileLogic;

        public FileController(IMapper mapper,
            IFileLogic fileLogic)
        {
            _mapper = mapper;
            _fileLogic = fileLogic;
        }

        [HttpGet(ApiRoutes.FileRoute.Get)]
        public async Task<IActionResult> Get([FromQuery] MainEntityQuery query)
        {
            //Validate here
            var entityByUserIdfilter = _mapper.Map<ByMainEntityFilter>(query);
            entityByUserIdfilter.UserId = HttpContext.GetUserId();
            var response = await _fileLogic.Get(entityByUserIdfilter);
            
            return Ok(response);
        }

        [HttpPost(ApiRoutes.FileRoute.Create)]
        public async Task<IActionResult> Create([FromBody] NoteCreateRequest request)
        {
            var topicCreate = _mapper.Map<NoteCreate>(request);
            topicCreate.UserId = HttpContext.GetUserId();
            var response = await _noteLogic.Create(topicCreate);
            return Ok(response);
        }

        [HttpPut(ApiRoutes.FileRoute.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid noteId, [FromBody] NoteUpdateRequest request)
        {
            var topicUpdate = _mapper.Map<NoteUpdate>(request);
            topicUpdate.UserId = HttpContext.GetUserId();
            topicUpdate.NoteId = noteId;
            var response = await _noteLogic.Update(topicUpdate);
            return Ok(response);
        }

        [HttpDelete(ApiRoutes.FileRoute.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid noteId)
        {
            var result = await _noteLogic.Delete(noteId, HttpContext.GetUserId());
            return Ok(result);
        }
    }
}
