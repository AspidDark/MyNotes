﻿using Microsoft.AspNetCore.Mvc;
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
            var response = await _startingPageLogic.Get(HttpContext.GetUserId());
            return Ok(response);
        }
    }
}
