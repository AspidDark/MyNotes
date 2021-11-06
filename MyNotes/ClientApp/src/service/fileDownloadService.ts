import axios from "axios";
import * as paths from '../Consts/PathConsts'
import {GetDependentEntityDto} from "../Dto/Dtos";

class FileDownloadservice{

    async downloadFile(downloadFileRequest:GetDependentEntityDto){
        const response= await axios.get(this.getQuey(downloadFileRequest), 
        {
            method: 'GET',
            responseType: 'blob'
        });
//Fix this TODO
        let contentDisposition = response.headers["content-disposition"];
        let firstFileName=contentDisposition.indexOf('=');
        let cuted=contentDisposition.slice(firstFileName+1);
        let lastIndexOf=cuted.indexOf(';');
        let fileName=cuted.substring(0, lastIndexOf);

        const downloadUrl = window.URL.createObjectURL(new Blob([response.data]));

        const link = document.createElement('a');

        link.href = downloadUrl;

        link.setAttribute('download', fileName);

        document.body.appendChild(link);

        link.click();

        link.remove();
    }

    private getQuey(downloadFileRequest:GetDependentEntityDto): string{
        let requestPath:string =paths.basePath+paths.filePath+`?entityId=${downloadFileRequest.entityId}&mainEntityId=${downloadFileRequest.mainEntityId}`;
        return requestPath;

    }
}

export default FileDownloadservice