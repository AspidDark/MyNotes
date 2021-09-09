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
    public class NoteLogic : INoteLogic
    {
        private readonly INoteContract _noteContract;
        private readonly IParagraphContract _paragraphContract;
        private readonly IParagraphLogic _paragraphLogic;
        private readonly IMapper _mapper;
        private readonly ILogger<NoteLogic> _logger;

        public NoteLogic(INoteContract noteContract,
            IParagraphContract paragraphContract,
            IParagraphLogic paragraphLogic,
            IMapper mapper,
            ILogger<NoteLogic> logger)
        {
            _noteContract = noteContract;
            _paragraphContract = paragraphContract;
            _paragraphLogic = paragraphLogic;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BaseResponse> Get(ByMainEntityFilter filter)
        {
            if (filter.EntityId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.entityIdEmpty);
            }

            if (filter.MainEntityId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.mainEntityIdIsEmpty);
            }

            if (filter.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            try
            {
                if (!await IsMainEntityAccessAllowed(filter.MainEntityId, filter.UserId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }

                var (result, note) = await GetNote(filter.EntityId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.noNote);
                }

                var mapResult = _mapper.Map<NoteDto>(note);

                return new Response<NoteDto>(mapResult)
                {
                    Result = true
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10011");
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

                var result = await _noteContract.GetListByParagraph(filter.UserId, filter.EntityId,
                    paginationFilter.PageSize,
                    paginationFilter.PageSize * paginationFilter.PageNumber);

                var responseBody = _mapper.Map<List<NoteDto>>(result);

                return new Response<List<NoteDto>>(responseBody) { Result = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10012");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponse> Create(NoteCreate noteCreate)
        {
            if (noteCreate.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (noteCreate.ParagraphId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.paragraphEmpty);
            }

            if (!await IsMainEntityAccessAllowed(noteCreate.ParagraphId, noteCreate.UserId))
            {
                return ErrorHelper.ErrorResult(Messages.noAccess);
            }

            try
            {
                var entity = _mapper.Map<Note>(noteCreate);
                var resultEntity = await _noteContract.Add(entity);
                return new Response<AddEntityResponseDto>(new AddEntityResponseDto { Id = resultEntity.Id }) { Result = resultEntity.Result };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10013");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponse> Update(NoteUpdate entity)
        {
            if (entity.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (entity.NoteId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.noteIdEmpty);
            }

            try
            {
                var (result, note) = await GetNote(entity.NoteId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.noNote);
                }

                if (!await IsMainEntityAccessAllowed(note.ParagraphId, entity.UserId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }
                note.Message = entity.Message;

                var upateResult = await _noteContract.Update(note);
                return new Response<Note>(upateResult) { Result = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10014");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponse> Delete(Guid noteId, Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (noteId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.noteIdEmpty);
            }

            try
            {
                var (result, entity) = await GetNote(noteId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.noParagraph);
                }

                if (!await IsMainEntityAccessAllowed(entity.ParagraphId, userId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }

                var deleteResult = await _noteContract.Remove(userId, noteId);
                return new BaseResponse { Result = deleteResult };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10015");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        private async Task<bool> IsMainEntityAccessAllowed(Guid mainEntityId, Guid userId)
        {
            var paragraph = await _paragraphContract.Get(mainEntityId);
            if (paragraph is null)
            {
                return false;
            }
            return await _paragraphLogic.IsAccessAllowed(paragraph, userId);
        }

        private async Task<(bool, Note)> GetNote(Guid noteId)
        {
            var entity = await _noteContract.Get(noteId);
            if (entity is null)
            {
                return (false, null);
            }
            return (true, entity);

        }
    }
}