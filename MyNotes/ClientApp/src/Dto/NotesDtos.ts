import {BaseBodyDto} from "./Dtos";

export interface NoteDto extends BaseBodyDto{
    name:string
    message:string,
    paragraphId:string
}

export interface AddNoteDto{
    message:string,
    name:string,
    paragraphId:string
}

export interface UpdateNoteDto{
    message:string,
    name:string,
    id:string
}

