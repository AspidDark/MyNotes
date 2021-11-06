import {
    Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Box,
    Textarea,
    Text 
  } from "@chakra-ui/react";

import { StartingPageDto } from "../../Dto/startingPageDto";
import {ParagraphDto} from '../../Dto/ParagraphDto'


 /*function ExtendedAccordionPanel(event:any, x:StartingPageDto, y:ParagraphDto){

   async function clicker(event:any, mainEntityId:string, entityId:string){
        const noteApi= new NoteApi();
        const result = await noteApi.getNote({entityId:entityId, mainEntityId:mainEntityId});
    
        if(!result.result){
          console.log(result.data);
          return;
        }
        let dataResult=result.data as NoteDto;
        setTextField(dataResult.message);
        console.log(dataResult);
        }
}*/