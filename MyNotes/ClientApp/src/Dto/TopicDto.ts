import {BaseBodyDto} from "./Dtos";

export interface TopicDto extends BaseBodyDto{
    name:string
}

export interface AddTopicDto {
    name:string
}

export interface UpdateTopicDto {
    name:string,
    topicId:string
}