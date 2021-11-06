import axios from "axios";
import * as paths from '../Consts/PathConsts'

interface UploadFileResponse {
    success: boolean,
    message: string
}

class FileUploadService 
{
    private file: File

    constructor(file: File) {
        this.file = file
    }

    static getFileExtension(fileName: string): string {
        const fileNames: Array<string> = fileName.split('.')

        if (fileNames.length === 0) {
            return ''
        }

        return fileNames[fileNames.length - 1]
    }
 
    async uploadFile(): Promise<UploadFileResponse> {
        try {
        const responseJson = await axios.post(paths.basePath + paths.filePath, this.getFormData());

        console.log(responseJson);
        }catch (ex) {
            console.log(ex);
            return {
                success: false,
                message: 'Uploaded Error'
            }
          }
        
        return {
            success: true,
            message: 'Uploaded Successfully'
        }
    }

    private getFormData(): FormData {
        const formData = new FormData()
        formData.append('formFile', this.file);
        formData.append("paragraphId", "c2f8bbf7-0564-4ad6-9a4d-4925a037e153");
        return formData
    }
}

export default FileUploadService