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
        public async Task<IActionResult> Create([FromBody] FileCreateRequest request)
        {
            FileCreate fileCreate = new()
            {
                UserId = HttpContext.GetUserId(),
                FileBody=request.FileBody,
                ParagraphId=request.ParagraphId
            };
            var response = await _fileLogic.Create(fileCreate);
            return Ok(response);
        }

        [HttpDelete(ApiRoutes.FileRoute.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid fileId)
        {
            var result = await _fileLogic.Delete(fileId, HttpContext.GetUserId());
            return Ok(result);
        }



        [HttpGet(ApiRoutes.FileMessageRoute.Get)]
        public async Task<IActionResult> GetFileMessage([FromQuery] MainEntityQuery query)
        {
            //Validate here
            var entityByUserIdfilter = _mapper.Map<ByMainEntityFilter>(query);
            entityByUserIdfilter.UserId = HttpContext.GetUserId();
            var response = await _fileLogic.GetMesage(entityByUserIdfilter);
            return Ok(response);
        }


        [HttpPost(ApiRoutes.FileMessageRoute.Create)]
        public async Task<IActionResult> CreateFileMessage([FromBody] FileMessageUpdateRequest request)
        {
            var fileMessageUpdate = _mapper.Map<FileMessageUpdate>(request);
            fileMessageUpdate.UserId = HttpContext.GetUserId();
            var response = await _fileLogic.CreateOrUpdateMessage(fileMessageUpdate);
            return Ok(response);
        }

        [HttpDelete(ApiRoutes.FileMessageRoute.Delete)]
        public async Task<IActionResult> DeleteMessage([FromRoute] Guid fileId)
        {
            var result = await _fileLogic.DeleteMessage(fileId, HttpContext.GetUserId());
            return Ok(result);
        }
    }
}
