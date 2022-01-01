import CRUDRequestHelper from '../service/CRUDRequestHelper'
import * as path from '../Consts/PathConsts';
import {BaseDto, AddEntityDto} from '../Dto/Dtos';
import {PaginatonWithMainEntity} from '../Dto/Pagination';
import {AddNoteDto, NoteDto, UpdateNoteDto} from '../Dto/NotesDtos'
import {GetDependentEntityDto} from '../Dto/Dtos'

class NoteApi{
    private uri:string=path.basePath+path.notePath;

    async getNote(dependentEntity:GetDependentEntityDto) :Promise<BaseDto<NoteDto>|BaseDto<string>>{
        const apiService = new CRUDRequestHelper();
        const path=this.uri+`?entityId=${dependentEntity.entityId}&mainEntityId=${dependentEntity.mainEntityId}`;
        const resultApi=await apiService.getRequest(path);
        if(!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
       let resultBody=this.mapToParagraph(resultApi.data);

        let response:BaseDto<NoteDto>={
            result:true,
            data:resultBody
        }
        return response;
    }

    async getNotes(pagination:PaginatonWithMainEntity):Promise<BaseDto<NoteDto[]>|BaseDto<string>>{
        const apiService = new CRUDRequestHelper();
        const path=this.uri+`s?pageNumber=${pagination.pageNumber}&pageSize=${pagination.pageSize}&paragraphId=${pagination.mainEntityId}`;
        const resultApi=await apiService.getRequest(path);
        if(!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
        let resultBody:NoteDto[]=resultApi.data.map((x: any)=>this.mapToParagraph(x));

        let response:BaseDto<NoteDto[]>={
            result:true,
            data:resultBody
        }
        return response;
    }

    async postNote(entity:AddNoteDto):Promise<BaseDto<AddEntityDto>|BaseDto<string>>{

        const apiService = new CRUDRequestHelper();
        const resultApi = await apiService.postRequest({url:this.uri, data: entity});
        if(!resultApi||!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }

        let addedEntity:AddEntityDto={
            id:resultApi.data.id
        }

        let result:BaseDto<AddEntityDto>={
            result:true,
            data:addedEntity
        }
        return result;
    }

    async deleteNote(entityId:string):Promise<BaseDto<string>>{
        const path=this.uri+`/${entityId}`;
        const apiService = new CRUDRequestHelper();
        const resultApi = await apiService.deleteRequest(path);
        if(!resultApi||!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
        let result:BaseDto<string>={
            result:true,
            data:resultApi.data
        }
        return result;
    }

    async updateNote(updateEntity:UpdateNoteDto):Promise<BaseDto<UpdateNoteDto>|BaseDto<string>>{
        const path=this.uri+`/${updateEntity.id}`;
        const apiService = new CRUDRequestHelper();
        const resultApi = await apiService.updateRequest({url:path, data:updateEntity});
        if(!resultApi||!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
        let result:BaseDto<UpdateNoteDto>={
            result:true,
            data:resultApi.data
        }
        return result;
    }


    private mapToParagraph(resultApi:any):NoteDto{
        let resultBody : NoteDto={
            name:resultApi.name,
            id:resultApi.id,
            message:resultApi.message,
            createDate:resultApi.createDate,
            editDate:resultApi.editDate,
            ownerId:resultApi.ownerId,
            paragraphId:resultApi.paragraphId
        }
        return resultBody;
    }
}

export default NoteApi;