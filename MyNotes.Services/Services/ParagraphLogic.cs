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
    public class ParagraphLogic : IParagraphLogic
    {
        private readonly IParagraphContract _paragraphContract;
        private readonly ITopicContract _topicContract;
        private readonly ITopicLogic _topicLogic;
        private readonly IAccessToEntity _accessToEntity;
        private readonly IMapper _mapper;
        private readonly ILogger<ParagraphLogic> _logger;
        public ParagraphLogic(IParagraphContract paragraphContract,
            ITopicContract topicContract,
            ITopicLogic topicLogic,
            IAccessToEntity accessToEntity,
            IMapper mapper,
            ILogger<ParagraphLogic> logger)
        {
            _paragraphContract = paragraphContract;
            _topicContract = topicContract;
            _topicLogic = topicLogic;
            _accessToEntity = accessToEntity;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse> Get(ByEntityFilter entityByUserIdFilter)
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
                var (result, paragraph) = await GetParagraph(entityByUserIdFilter.EntityId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.noParagraph);
                }

                if (!await IsAccessAllowed(paragraph, entityByUserIdFilter.UserId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }

                var mapResult = _mapper.Map<ParagraphDto>(paragraph);
                return new Response<ParagraphDto>(mapResult)
                {
                    Result = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10006");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }

        }

        public async Task<BaseResponse> GetList(ByEntityFilter filter, PaginationFilter paginationFilter)
        {
            if (filter.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (filter.EntityId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.topicIdEmpty);
            }
            try
            {

                if (!await IsMainEntityAccessAllowed(filter.EntityId, filter.UserId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }
                var result = await _paragraphContract.GetListByTopic(filter.UserId, filter.EntityId,
                    paginationFilter.PageSize,
                    paginationFilter.PageSize * paginationFilter.PageNumber);

                var responseBody = _mapper.Map<List<ParagraphDto>>(result);

                return new Response<List<ParagraphDto>>(responseBody) { Result = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10007");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponse> Create(ParagraphCreate paragraphCreate)
        {
            if (paragraphCreate.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (paragraphCreate.TopicId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.topicIdEmpty);
            }

            if (!await IsMainEntityAccessAllowed(paragraphCreate.TopicId, paragraphCreate.UserId))
            {
                return ErrorHelper.ErrorResult(Messages.noAccess);
            }

            try
            {
                var entity = _mapper.Map<Paragraph>(paragraphCreate);
                var result = await _paragraphContract.Add(entity);
                return new BaseResponse { Result = result };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10008");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponse> Update(ParagraphUpdate entity)
        {
            if (entity.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (entity.ParagraphId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.paragraphEmpty);
            }

            try
            {
                var (result, paragraph) = await GetParagraph(entity.ParagraphId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.noParagraph);
                }
                if (!await IsAccessAllowed(paragraph, entity.UserId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }
                paragraph.Name = entity.Name;

                var upateResult = await _paragraphContract.Update(paragraph);
                return new Response<Paragraph>(upateResult) { Result = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10009");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponse> Delete(Guid paragraphId, Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (paragraphId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.paragraphEmpty);
            }

            try
            {
                var (result, entity) = await GetParagraph(paragraphId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.noParagraph);
                }

                if (!await IsAccessAllowed(entity, userId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }

                var deleteResult = await _paragraphContract.Remove(userId, paragraphId);
                return new BaseResponse { Result = deleteResult };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10010");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<bool> IsAccessAllowed(Paragraph entity, Guid userId)
        {
            if (entity.OwnerId != userId)
            {
                var userAccess = await _accessToEntity.CheckAccessToEntity(entity.OwnerId,
                    userId, entity.Id);

                if (userAccess == Domain.Enums.AccessType.Closed)
                {
                    return false;
                }
            }
            return true;
        }

        private async Task<bool> IsMainEntityAccessAllowed(Guid mainEntityId, Guid userId)
        {
            var topic = await _topicContract.Get(mainEntityId);
            if (topic is null)
            {
                return false;
            }
            return await _topicLogic.IsAccessAllowed(topic, userId);
        }

        private async Task<(bool, Paragraph)> GetParagraph(Guid paragraphId)
        {
            var entity = await _paragraphContract.Get(paragraphId);
            if (entity is null)
            {
                return (false, null);
            }
            return (true, entity);
        }

    }
}
