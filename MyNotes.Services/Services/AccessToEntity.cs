using MyNotes.Domain.Contracts.Rights;
using MyNotes.Domain.Enums;
using MyNotes.Services.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyNotes.Services.Services
{
    public class AccessToEntity : IAccessToEntity
    {
        private readonly IGlobalRightContract _globalRightContract;
        private readonly ILocalRightContract _localRightContract;

        public AccessToEntity(IGlobalRightContract globalRightContract,
            ILocalRightContract localRightContract)
        {
            _globalRightContract = globalRightContract;
            _localRightContract = localRightContract;
        }

        public async Task<AccessType> CheckAccessToEntity(
            Guid ownerId, 
            Guid userId, 
            Guid resourceId)
        {
            var globalAccessType = await _globalRightContract.GetByOwnerAndRequesterId(ownerId, userId);
            if (globalAccessType is not null)
            {
                return globalAccessType.AccessType;
            }

            var localRight = await _localRightContract.GetUserRightToResource(ownerId, userId, resourceId);
            if (localRight is not null)
            {
                return localRight.AccessType;
            }
            return AccessType.Closed;
        }

        //public async Task<List<Guid>> AllowedEntities()
        //{ 
        
        //}
    }
}
