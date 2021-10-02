using AutoMapper;
using MyNotes.Contracts.V1;
using MyNotes.Contracts.V1.Response;
using MyNotes.Services.InternalDto;
using MyNotes.Services.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Services.Services
{
    public class StartingPageLogic : IStartingPageLogic
    {
        private readonly ITopicLogic _topicLogic;
        private readonly IParagraphLogic _paragraphLogic;
        private readonly IMapper _mapper;

        public StartingPageLogic(ITopicLogic topicLogic, IParagraphLogic paragraphLogic, IMapper mapper)
        {
            _topicLogic = topicLogic;
            _paragraphLogic = paragraphLogic;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Get(Guid userId)
        {
            BaseUserIdFilter baseUserIdFilter = new() { UserId = userId };
            PaginationFilter paginationFilter = new() { PageSize = 50, PageNumber = 0 };

            var topics = await _topicLogic.GetList(baseUserIdFilter, paginationFilter);

            if (!topics.Result)
            {
                return new BaseResponse() { Result = false, Message = "No Topics created" };
            }

            var topicResult = topics as Response<List<TopicDto>>;
            if (topicResult is null || topicResult.Data.Count == 0)
            {
                return new BaseResponse() { Result = false, Message = "No Topics created" };
            }

            List<StartingPageDto> startingPageDtos = new();
            foreach (var item in topicResult.Data)
            {
                ByEntityFilter byEntityFilter = new() { EntityId = item.Id, UserId = userId };
                StartingPageDto startingPageDataDto = _mapper.Map<StartingPageDto>(item);

                var paragraphs = await _paragraphLogic.GetList(byEntityFilter, paginationFilter);

                if (paragraphs.Result)
                {
                    var paragraphsResult = paragraphs as Response<List<ParagraphDto>>;
                    startingPageDataDto.Paragraphs = paragraphsResult.Data;
                }
                startingPageDtos.Add(startingPageDataDto);
            }

            return new Response<List<StartingPageDto>>(startingPageDtos) { Result = true };
        }
    }
}
