import CRUDRequestHelper from '../service/CRUDRequestHelper'
import * as path from '../Consts/PathConsts';
import {TopicDto, AddTopicDto, UpdateTopicDto} from '../Dto/TopicDto';
import {BaseDto, AddEntityDto} from '../Dto/Dtos';
import {Pagination} from '../Dto/Pagination';

class TopicApi{ 

    private uri:string=path.basePath+path.topicPath;

    async getTopic(topicId:string) :Promise<BaseDto<TopicDto>|BaseDto<string>>{
        const apiService = new CRUDRequestHelper();
        const path=this.uri+`?entityId=${topicId}`;
        const resultApi=await apiService.getRequest(path);
        if(!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
       let resultBody=this.mapToTopic(resultApi.data);

        let response:BaseDto<TopicDto>={
            result:true,
            data:resultBody
        }
        return response;
    }

    async getTopics(pagination:Pagination):Promise<BaseDto<TopicDto[]>|BaseDto<string>>{
        const apiService = new CRUDRequestHelper();
        const path=this.uri+`s?pageNumber=${pagination.pageNumber}&pageSize=${pagination.pageSize}`;
        const resultApi=await apiService.getRequest(path);
        if(!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
        let resultBody:TopicDto[]=resultApi.data.map((x: any)=>this.mapToTopic(x));

        let response:BaseDto<TopicDto[]>={
            result:true,
            data:resultBody
        }
        return response;
    }

    async postTopic(topic:AddTopicDto):Promise<BaseDto<AddEntityDto>|BaseDto<string>>{

        const apiService = new CRUDRequestHelper();
        const resultApi = await apiService.postRequest({url:this.uri, data: topic});
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

    async deleteTopic(topicId:string):Promise<BaseDto<string>>{
        const path=this.uri+`/${topicId}`;
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

    async updateTopic(updateTopic:UpdateTopicDto):Promise<BaseDto<TopicDto>|BaseDto<string>>{
        const path=this.uri+`/${updateTopic.topicId}`;
        const apiService = new CRUDRequestHelper();
        const resultApi = await apiService.updateRequest({url:path, data:updateTopic.name});
        if(!resultApi||!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
        let result:BaseDto<TopicDto>={
            result:true,
            data:resultApi.data
        }
        return result;
    }


    private mapToTopic(resultApi:any):TopicDto{
        let resultBody : TopicDto={
            id:resultApi.id,
            name:resultApi.name,
            createDate:resultApi.createDate,
            editDate:resultApi.editDate,
            ownerId:resultApi.ownerId
        }
        return resultBody;
    }



    private getFormData(object:any):FormData{
        var form_data = new FormData();
        for ( var key in object ) {
            form_data.append(key, object[key]);
        } 
        return form_data;
    }

}
export default TopicApi;