import {
    Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Box
  } from "@chakra-ui/react";
import { ChangeEvent, ReactComponentElement, SyntheticEvent, useEffect, useState } from 'react'
import TopicApi from '../Apis/topicApi'
import {TopicDto, AddTopicDto} from '../Dto/TopicDto';
import {StartingPageDto} from '../Dto/startingPageDto'
import StartingPageApi from '../Apis/startingPageApi'
import {BaseDto} from '../Dto/Dtos';

function TempAAA(){
    let toRender:string[]=['aaa','bbb','ccc']; 

    var okResult= toRender.map((x:string)=> 
      <AccordionItem>
         <h2>
      <AccordionButton>
        <Box flex="1" textAlign="left">
          {x}
        </Box>
        <AccordionIcon />
      </AccordionButton>
    </h2>
     </AccordionItem> );
    return (<Accordion>{okResult}</Accordion>);
}

export default TempAAA;