using AutoMapper;
using MyNotes.Contracts.V1;
using MyNotes.Contracts.V1.Response;
using MyNotes.Domain.Contracts.Core;
using MyNotes.Domain.Contracts.Rights;
using MyNotes.Domain.Entities.Core;
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

        public TopicLogic(ITopicContract topicContract, IAccessToEntity accessToEntity, IMapper mapper)
        {
            _topicContract = topicContract;
            _accessToEntity = accessToEntity;
            _mapper = mapper;
        }

        public async Task<BaseResponseDto> Get(EntityByUserIdFilter entityByUserIdFilter)
        {
            try
            {
                bool entityParseResult = Guid.TryParse(entityByUserIdFilter.EntityId, out Guid entityId);
                if (!entityParseResult)
                {
                    return null;
                }
                bool userParseResult = Guid.TryParse(entityByUserIdFilter.UserId, out Guid userId);
                if (!userParseResult)
                {
                    return null;
                }

                var topic = await _topicContract.Get(entityId);
                if (topic is null)
                {
                    return ErrorResult("No Topic");
                }

                if (topic.OwnerId == userId)
                {
                    var mapResult = _mapper.Map<TopicDto>(topic);
                    var response = new Response<TopicDto>(mapResult)
                    {
                        Result = true
                    };

                    return response;
                }



            }
            catch (Exception e)
            { 
            
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



        private static BaseResponseDto ErrorResult(string message) 
        {
            return new BaseResponseDto { Result = false, Message=message };
        }
    }
}
