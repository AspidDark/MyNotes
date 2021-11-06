import axios from "axios";

export interface RequestData {
    url:string,
    data: any,
}

export interface ResponseData{
    success:boolean,
    data:any
}
 
class CRUDRequestHelper{
    
        async getRequest (request:string) : Promise<ResponseData>{
            try{
                const responseJson = await axios.get(request);
                if(!responseJson){
                    return  this.errorObject();
                }
                if(!responseJson.data.result){
                    return this.errorObject(responseJson.data.message)
                }
                return {success:responseJson.data.result, data:responseJson.data.data}
    
             }catch (ex) {
                console.log(ex);
                return  this.errorObject();
            }
        }

    async postRequest (request:RequestData) : Promise<ResponseData>{
        try{
            const responseJson = await axios.post(request.url, request.data);
            if(!responseJson){
                return  this.errorObject();
            }
            if(!responseJson.data.result){
                return this.errorObject(responseJson.data.message)
            }
            return {success:responseJson.data.result, data:responseJson.data.data}

        }catch (ex) {
            console.log(ex);
            return  this.errorObject();
        }
    }

    async deleteRequest (request:string) : Promise<ResponseData>{
        try{
            const responseJson = await axios.delete(request);
            if(!responseJson){
                return  this.errorObject();
            }
            if(!responseJson.data.result){
                return this.errorObject(responseJson.data.message)
            }
            return {success:responseJson.data.result, data:responseJson.data.message}

        }catch (ex) {
            console.log(ex);
            return  this.errorObject();
        }
    }

    async updateRequest (request:RequestData) : Promise<ResponseData>{
        try{
            const responseJson = await axios.put(request.url, {name: request.data});
            if(!responseJson){
                return  this.errorObject();
            }
            if(!responseJson.data.result){
                return this.errorObject(responseJson.data.message)
            }
            return {success:responseJson.data.result, data:responseJson.data.data}

        }catch (ex) {
            console.log(ex);
            return  this.errorObject();
        }
    }

    private errorObject(message:string='Error'):ResponseData {
        return {
            success: false,
            data:message
        }
    }
}
export default CRUDRequestHelper;