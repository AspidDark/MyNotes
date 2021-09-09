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
using System.Threading.Tasks;


namespace MyNotes.Services.Services
{
    public class FileLogic : IFileLogic
    {
        private readonly IFileEntityContract _fileEntityContract;
        private readonly IParagraphContract _paragraphContract;
        private readonly IParagraphLogic _paragraphLogic;
        private readonly IFileHelper _fileHelper;
        private readonly IMapper _mapper;
        private readonly ILogger<FileLogic> _logger;


        public FileLogic(IFileEntityContract fileEntityContract,
            IParagraphContract paragraphContract,
            IParagraphLogic paragraphLogic,
            IFileHelper fileHelper,
            IMapper mapper,
            ILogger<FileLogic> logger)
        {
            _fileEntityContract = fileEntityContract;
            _paragraphContract = paragraphContract;
            _paragraphLogic = paragraphLogic;
            _fileHelper = fileHelper;
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

                var (result, fileData) = await GetFileData(filter.EntityId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.fileMissing);
                }

                var fileSream = _fileHelper.GetFileStream(fileData.SavedFileName);
                var response = _mapper.Map<FileEntityResponseDto>(fileData);
                response.FileEntity = fileSream;

                return new Response<FileEntityResponseDto>(response) { Result = true };

            }
            catch (Exception e)
            {
                _logger.LogError(e, "10016");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponse> Create(FileCreate request)
        {
            if (request.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (request.ParagraphId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.paragraphEmpty);
            }

            if (!await IsMainEntityAccessAllowed(request.ParagraphId, request.UserId))
            {
                return ErrorHelper.ErrorResult(Messages.noAccess);
            }

            try
            {
                //имя файла
                var fileName = request.FileBody.FileName;
               // var name = request.FileBody.Name;
                //Расширенеи файла
                string ext = System.IO.Path.GetExtension(fileName);
                var fileId = Guid.NewGuid();

                //Имя файла для сохранения
                string fileSaveName = fileId.ToString()+fileName;

                if (!await _fileHelper.SaveFile(request.FileBody, fileSaveName))
                {
                    return ErrorHelper.ErrorResult(Messages.fileSaveError);
                }

                FileEntity fileEntity = new()
                {
                    FileName = fileName,
                    Id = fileId,
                    OwnerId = request.UserId,
                    ParagraphId = request.ParagraphId,
                    SavedFileName = fileSaveName
                };

                var saveResult = await _fileEntityContract.Add(fileEntity);
                if (!saveResult.Result)
                {
                    return ErrorHelper.ErrorResult(Messages.fileEntitySaveError);
                }

                return new Response<AddEntityResponseDto>(new AddEntityResponseDto { Id = saveResult.Id }) { Result = saveResult.Result };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10017");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponse> Delete(Guid fileId, Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (fileId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.noteIdEmpty);
            }

            try
            {
                var (result, entity) = await GetFileData(fileId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.fileMissing);
                }

                if (!await IsMainEntityAccessAllowed(entity.ParagraphId, userId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }

                var deleteResult = await _fileEntityContract.Remove(userId, fileId);

                var deleteTask = Task.Factory.StartNew(() => _fileHelper.DeleteFile(entity.SavedFileName));
                //deleteTask.Wait();

                return new BaseResponse { Result = deleteResult };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10019");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        /// <summary>
        /// Only picrure explanation message row already exists
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<BaseResponse> CreateOrUpdateMessage(FileMessageUpdate request)
        {
            if (request.UserId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (request.ParagraphId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.paragraphEmpty);
            }

            if (request.FileId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.fileIdEmpty);
            }

            if (!await IsMainEntityAccessAllowed(request.ParagraphId, request.UserId))
            {
                return ErrorHelper.ErrorResult(Messages.noAccess);
            }

            try
            {
                var (result, fileData) = await GetFileData(request.FileId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.fileMissing);
                }

                fileData.Explanation = request.Explanation;

                var upateResult = await _fileEntityContract.Update(fileData);

                FileMessageResponseDto response = new()
                {
                    Id=upateResult.Id,
                    ParagraphId= upateResult.ParagraphId,
                    Message= upateResult.Explanation
                };

                return new Response<FileMessageResponseDto>(response) { Result = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10020");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }

        }

        public async Task<BaseResponse> GetMesage(ByMainEntityFilter filter)
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

                var (result, fileData) = await GetFileData(filter.EntityId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.fileMissing);
                }
                FileMessageResponseDto response = new ()
                { 
                    Id=fileData.Id,
                    ParagraphId = fileData.ParagraphId,
                    Message= fileData.Explanation
                };

                return new Response<FileMessageResponseDto>(response) { Result = true };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10021");
                return ErrorHelper.ErrorResult(Messages.somethingWentWrong);
            }
        }

        public async Task<BaseResponse> DeleteMessage(Guid fileId, Guid userId)
        {
            if (userId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.userIdEmpty);
            }

            if (fileId == Guid.Empty)
            {
                return ErrorHelper.ErrorResult(Messages.noteIdEmpty);
            }

            try
            {
                var (result, entity) = await GetFileData(fileId);
                if (!result)
                {
                    return ErrorHelper.ErrorResult(Messages.fileMissing);
                }

                if (!await IsMainEntityAccessAllowed(entity.ParagraphId, userId))
                {
                    return ErrorHelper.ErrorResult(Messages.noAccess);
                }
                entity.Explanation = string.Empty;

                var deleteResult = await _fileEntityContract.Update(entity);

                return new BaseResponse { Result = string.IsNullOrWhiteSpace(deleteResult.Explanation) };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "10022");
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

        private async Task<(bool, FileEntity)> GetFileData(Guid fileId)
        {
            var entity = await _fileEntityContract.Get(fileId);
            if (entity is null)
            {
                return (false, null);
            }
            return (true, entity);
        }
    }
}
