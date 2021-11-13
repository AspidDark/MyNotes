import {
    Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Box,
    Textarea,
    Text, 
    Link 
  } from "@chakra-ui/react";
import React, { ChangeEvent, ReactComponentElement, SyntheticEvent, useEffect, useState } from 'react'
import TopicApi from '../Apis/topicApi'
import {StartingPageDto} from '../Dto/startingPageDto'
import StartingPageApi from '../Apis/startingPageApi'
import {BaseDto} from '../Dto/Dtos';
import {ParagraphDto} from '../Dto/ParagraphDto'
import NoteApi from "../Apis/notesApi";
import { NoteDto } from "../Dto/NotesDtos";
import {PaginatonWithMainEntity} from '../Dto/Pagination';
import DataFunction from './InternalTypes/MainWindowTextData'


function TopicList(dataFunc:DataFunction){
  
  const [data, setData]=useState<JSX.Element[]>();
  const [isLoaded, setLoaded]=useState(false);

  useEffect(() => {
    renderListAsync();
  }, []);

  async function clicker(event:any, mainEntityId:string, entityId:string){
    const paginated : PaginatonWithMainEntity=
      {
        mainEntityId:entityId,
        pageNumber:0,
        pageSize:20
      }
    const noteApi= new NoteApi();
    const result = await noteApi.getNotes(paginated);

    if(!result.result){
      console.log(result.data);
      return;
    }
    dataFunc.dataFunc(result.data as NoteDto[]);
  }

  async function renderListAsync() {
    const requeatService=new StartingPageApi();
    const result = await requeatService.getStartingData();
    if(!result.result){
      setLoaded(false);
      return result.data as string;
    }
    let dataResult=result.data as StartingPageDto[];
    setLoaded(true);


    var okResult= dataResult.map((x:StartingPageDto)=> 
      <AccordionItem>
         <h2>
      <AccordionButton>
        <Box flex="0" textAlign="left" id={x.id}>
          {x.name}
        </Box>
        <AccordionIcon />
      </AccordionButton>
    </h2>
      {x.paragraphs.map((y:ParagraphDto)=> <AccordionPanel onClick={e=> clicker(e, x.id, y.id)} pb={4} elementId={y.id}><Link> {y.name}</Link></AccordionPanel>)}
     </AccordionItem> );
     setData(okResult);
    return okResult;
  }


    return(<>
      <Accordion>{data}</Accordion>
      </>
    );
}

export default TopicList;
// {x.paragraphs.map((y:ParagraphDto)=> <AccordionPanel pb={4}>{y.name}</AccordionPanel>)}