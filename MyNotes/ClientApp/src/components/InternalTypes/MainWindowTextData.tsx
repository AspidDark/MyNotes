import { NoteDto } from "../../Dto/NotesDtos";

export default interface DataFunction{
   dataFunc:(textData:NoteDto[], paragraphId:string )=>void;
}