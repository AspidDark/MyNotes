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
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;

namespace MyNotes.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StartingPageController : ControllerBase
    {
        private readonly IStartingPageLogic _startingPageLogic;

        public StartingPageController(IStartingPageLogic startingPageLogic)
        {
            _startingPageLogic = startingPageLogic;
        }

        [HttpGet(ApiRoutes.StartingPageRoute.Get)]
        public async Task<IActionResult> Get()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var response = await _startingPageLogic.Get(HttpContext.GetUserId());
            return Ok(response);
        }
    }
}
