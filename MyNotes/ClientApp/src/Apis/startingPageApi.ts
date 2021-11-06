import CRUDRequestHelper from '../service/CRUDRequestHelper'
import * as path from '../Consts/PathConsts';
import { StartingPageDto } from '../Dto/startingPageDto';
import {BaseDto} from '../Dto/Dtos';

class StartingPageApi{
    private uri:string=path.basePath+path.startingPageDataPath;

    async getStartingData() :Promise<BaseDto<StartingPageDto[]>|BaseDto<string>>{
        const apiService = new CRUDRequestHelper();
        const resultApi=await apiService.getRequest(this.uri);
        if(!resultApi.success){
            let errorResult:BaseDto<string>={
                result:false,
                data:resultApi.data
            }
            return errorResult;
        }
        let resultBody:StartingPageDto[]=resultApi.data.map((x: any)=>this.mapToStartingPageDto(x));

        let response:BaseDto<StartingPageDto[]>={
            result:true,
            data:resultBody
        }
        return response;
    }

    private mapToStartingPageDto(resultApi:any):StartingPageDto{
        let resultBody : StartingPageDto={
            id:resultApi.id,
            name:resultApi.name,
            createDate:resultApi.createDate,
            editDate:resultApi.editDate,
            ownerId:resultApi.ownerId,
            paragraphs:resultApi.paragraphs
        }
        return resultBody;
    }

}

export default StartingPageApi;