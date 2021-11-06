import {ParagraphDto } from "./ParagraphDto";
import { TopicDto } from "./TopicDto";

export interface StartingPageDto extends TopicDto{
    paragraphs : ParagraphDto[]
}


