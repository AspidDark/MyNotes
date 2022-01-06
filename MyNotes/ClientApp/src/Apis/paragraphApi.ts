import CRUDRequestHelper from '../service/CRUDRequestHelper'
import * as path from '../Consts/PathConsts';
import {ParagraphDto, AddParagraphDto, UpdateParagraphDto} from '../Dto/ParagraphDto';
import {BaseDto, AddEntityDto} from '../Dto/Dtos';
import {PaginatonWithMainEntity} from '../Dto/Pagination';

class ParagraphApi{
    private uri:string=path.basePath+path.paragraphpath;

    async getParagraph(entityId:string) :Promise<BaseDto<ParagraphDto>|BaseDto<string>>{
        const apiService = new CRUDRequestHelper();
        const path=this.uri+`?entityId=${entityId}`;
        const resultApi=await apiService.getRequest(path);
        if(!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
       let resultBody=this.mapToParagraph(resultApi.data);

        let response:BaseDto<ParagraphDto>={
            result:true,
            data:resultBody
        }
        return response;
    }

    async getParagraphs(pagination:PaginatonWithMainEntity):Promise<BaseDto<ParagraphDto[]>|BaseDto<string>>{
        const apiService = new CRUDRequestHelper();
        const path=this.uri+`s?pageNumber=${pagination.pageNumber}&pageSize=${pagination.pageSize}&topicId=${pagination.mainEntityId}`;
        const resultApi=await apiService.getRequest(path);
        if(!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
        let resultBody:ParagraphDto[]=resultApi.data.map((x: any)=>this.mapToParagraph(x));

        let response:BaseDto<ParagraphDto[]>={
            result:true,
            data:resultBody
        }
        return response;
    }

    async postParagraph(entity:AddParagraphDto):Promise<BaseDto<AddEntityDto>|BaseDto<string>>{

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

    async deleteParagraph(entityId:string):Promise<BaseDto<string>>{
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

    async updateParagraph(updateEntity:UpdateParagraphDto):Promise<BaseDto<ParagraphDto>|BaseDto<string>>{
        const path=this.uri+`/${updateEntity.id}`;
        const apiService = new CRUDRequestHelper();
        const resultApi = await apiService.updateRequest({url:path, data:{name:updateEntity.name}});
        if(!resultApi||!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
        let result:BaseDto<ParagraphDto>={
            result:true,
            data:resultApi.data
        }
        return result;
    }

    private mapToParagraph(resultApi:any):ParagraphDto{
        let resultBody : ParagraphDto={
            id:resultApi.id,
            name:resultApi.name,
            createDate:resultApi.createDate,
            editDate:resultApi.editDate,
            ownerId:resultApi.ownerId,
            topicId:resultApi.topicId
        }
        return resultBody;
    }

}

export default ParagraphApi;