import {BaseBodyDto} from "./Dtos";

export interface ParagraphDto extends BaseBodyDto{
    name:string,
    topicId:string
}

export interface AddParagraphDto{
    name:string,
    topicId:string
}

export interface UpdateParagraphDto{
    name:string,
    id:string
}