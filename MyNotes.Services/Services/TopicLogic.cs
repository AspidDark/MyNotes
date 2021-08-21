using AutoMapper;
using Microsoft.Extensions.Logging;
using MyNotes.Contracts.V1;
using MyNotes.Contracts.V1.Response;
using MyNotes.Domain.Contracts.Core;
using MyNotes.Domain.Contracts.Rights;
using MyNotes.Domain.Entities.Core;
using MyNotes.Services.Helpers;
using MyNotes.Services.InternalDto;
using MyNotes.Services.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Services.Services
{

    public class TopicLogic
    {
        private readonly ITopicContract _topicContract;
        private readonly IAccessToEntity _accessToEntity;
        private readonly IMapper _mapper;
        private readonly ILogger<TopicLogic> _logger;

        public TopicLogic(ITopicContract topicContract, IAccessToEntity accessToEntity, IMapper mapper, ILogger<TopicLogic> logger)
        {
            _topicContract = topicContract;
            _accessToEntity = accessToEntity;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponseDto> Get(EntityByUserIdFilter entityByUserIdFilter)
        {
            try
            {
                var (response, topic) = await VerifyParameters(entityByUserIdFilter.EntityId, entityByUserIdFilter.UserId);
                if (!response.Result)
                {
                    return response;
                }

                if (topic.OwnerId != entityByUserIdFilter.UserId)
                {
                    var userAccess = await _accessToEntity.CheckAccessToEntity(topic.OwnerId,
                        entityByUserIdFilter.UserId, entityByUserIdFilter.EntityId);

                    if (userAccess == Domain.Enums.AccessType.Closed)
                    {
                        return ErrorHelper.ErrorResult(Messages.noAccess);
                    }
                }

                var mapResult = _mapper.Map<TopicDto>(topic);
                return new Response<TopicDto>(mapResult)
                {
                    Result = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"10001");
                return ErrorHelper.ErrorResult(Messages.noAccess);
            }
        }

        public async Task<List<string>> GetList(BaseUserIdFilter baseUserIdFilter, PaginationFilter paginationFilter)
        {

            return null;
        }

        public async Task<string> Create(TopicCreate topicCreate)
        {

            return null;
        }

        public async Task<string> Update(Guid topicId, TopicUpdate topicUpdate)
        {

            return null;
        }

        public async Task<string> Delete(Guid topicId, Guid userId)
        {
            return null;
        }


        private async Task<(BaseResponseDto, Topic)> VerifyParameters(Guid topicId, Guid userId)
        {
            if (topicId == Guid.Empty)
            {
                return (ErrorHelper.ErrorResult(Messages.entityIdEmpty), null);
            }

            if (userId == Guid.Empty)
            {
                return (ErrorHelper.ErrorResult(Messages.userIdEmpty), null); ;
            }

            var topic = await _topicContract.Get(topicId);
            if (topic is null)
            {
                return (ErrorHelper.ErrorResult(Messages.noTopic), null);
            }

            return (new BaseResponseDto { Result = true }, topic);

        }
    }
}
