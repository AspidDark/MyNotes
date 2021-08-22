using AutoMapper;
using Microsoft.Extensions.Logging;
using MyNotes.Contracts.V1;
using MyNotes.Contracts.V1.Response;
using MyNotes.Domain.Contracts.Core;
using MyNotes.Domain.Entities.Core;
using MyNotes.Services.Helpers;
using MyNotes.Services.InternalDto;
using MyNotes.Services.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Services.Services
{

    public class TopicLogic : ITopicLogic
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
            if (entityByUserIdFilter.EntityId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.entityIdEmpty);
            }

            if (entityByUserIdFilter.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            try
            {
                var (result, topic) = await GetTopic(entityByUserIdFilter.EntityId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.noTopic);
                }

                if (!await IsAccessAllowed(topic, entityByUserIdFilter.UserId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }

                var mapResult = _mapper.Map<TopicDto>(topic);
                return new Response<TopicDto>(mapResult)
                {
                    Result = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10001");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponseDto> GetList(BaseUserIdFilter baseUserIdFilter, PaginationFilter paginationFilter)
        {
            if (baseUserIdFilter.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }
            try
            {
                var result = await _topicContract.GetList(baseUserIdFilter.UserId,
                    paginationFilter.PageSize,
                    paginationFilter.PageSize * paginationFilter.PageNumber);

                var responseBody = _mapper.Map<List<TopicDto>>(result);

                return new Response<List<TopicDto>>(responseBody) { Result = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10002");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        //public async Task<BaseResponseDto> GetAllowedList(Guid ownerId, Guid userId, PaginationFilter paginationFilter)
        //{
        //    if (ownerId == Guid.Empty || userId == Guid.Empty)
        //    {
        //        return ErrorHelper.ErrorResult(Messages.userIdEmpty);
        //    }


        //}

        public async Task<BaseResponseDto> Create(TopicCreate topicCreate)
        {
            try
            {
                var topic = _mapper.Map<Topic>(topicCreate);
                var result = await _topicContract.Add(topic);
                return new BaseResponseDto { Result = result };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10003");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponseDto> Update(TopicUpdate topicUpdate)
        {
            try
            {
                var (result, topic) = await GetTopic(topicUpdate.TopicId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.noTopic);
                }
                if (!await IsAccessAllowed(topic, topicUpdate.UserId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }
                topic.Name = topicUpdate.Name;

                var upateResult = await _topicContract.Update(topic);
                return new Response<Topic>(upateResult) { Result = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10004");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponseDto> Delete(Guid topicId, Guid userId)
        {
            try
            {
                var (result, topic) = await GetTopic(topicId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.noTopic);
                }

                if (!await IsAccessAllowed(topic, userId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }

                var deleteResult = await _topicContract.Remove(userId, topicId);
                return new BaseResponseDto { Result = deleteResult };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10005");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        private async Task<(bool, Topic)> GetTopic(Guid topicId)
        {
            var topic = await _topicContract.Get(topicId);
            if (topic is null)
            {
                return (false, null);
            }
            return (true, topic);

        }

        private async Task<bool> IsAccessAllowed(Topic topic, Guid userId)
        {
            if (topic.OwnerId != userId)
            {
                var userAccess = await _accessToEntity.CheckAccessToEntity(topic.OwnerId,
                    userId, topic.Id);

                if (userAccess == Domain.Enums.AccessType.Closed)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
