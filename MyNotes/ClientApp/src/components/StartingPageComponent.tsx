import {
    Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Box,
    Textarea,
    Text, 
    Link,
    Input 
  } from "@chakra-ui/react";
import { DeleteIcon, AddIcon, EditIcon } from '@chakra-ui/icons'
import React, { ChangeEvent, ReactComponentElement, SyntheticEvent, useEffect, useState } from 'react'
import TopicApi from '../Apis/topicApi'
import {StartingPageDto} from '../Dto/startingPageDto'
import StartingPageApi from '../Apis/startingPageApi'
import {AddEntityDto, BaseDto} from '../Dto/Dtos';
import {ParagraphDto} from '../Dto/ParagraphDto'
import NoteApi from "../Apis/notesApi";
import { NoteDto } from "../Dto/NotesDtos";
import {PaginatonWithMainEntity} from '../Dto/Pagination';
import DataFunction from './InternalTypes/MainWindowTextData';
import { AddTopicDto } from "../Dto/TopicDto";



function TopicList(dataFunc:DataFunction){
  
  const [data, setData]=useState<JSX.Element[]>();
  const [isLoaded, setLoaded]=useState(false);
  const [isAddedElement, setIsAddedElement]=useState(false);
  const [isAddClicked, setIsAddClicked]=useState(false);
 // const [newTopicName, setNewTopicName]=useState('');

  useEffect(() => {
    renderListAsync();
  }, [isAddedElement]);

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

  async function CeateTopic(value:string) {
    //setNewTopicName(value);
    if( value )
    {
      const requestService=new TopicApi();
      let addTopicDto:AddTopicDto={
       name:value
     }
      const result = await requestService.postTopic(addTopicDto);
          if(!result.result){
              let qqq=55;
              return;
          }
        let dataResult=result.data as AddEntityDto;
        let qqq2=66;
      }
      setIsAddedElement(!isAddedElement); 
      setIsAddClicked(false);
  }

  async function AddIconClicked(event:any) {
    if(isAddClicked)
    {
      return;      
    }
    setIsAddClicked(true);
    setData([SetInput(),...data as JSX.Element[]]);
  }

  function SetInput():JSX.Element {
    return ( <Input placeholder="small size" size="sm" /*onChange={e=>setNewTopicName(e.target.value)}*/ onBlur={e=>CeateTopic(e.target.value)}/>);
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
    <>
      <AccordionItem key={x.id}>
         <h2>
      <AccordionButton>
        <Box flex="0" textAlign="left" id={x.id}>
          {x.name}
        </Box>
        <AccordionIcon />
      </AccordionButton>
    </h2>
      {x.paragraphs.map((y:ParagraphDto)=> 
        <AccordionPanel onClick={e=> clicker(e, x.id, y.id)} pb={4} elementId={y.id} key={y.id} >
          <Link> {y.name}</Link>
        </AccordionPanel>
        
        )}
     </AccordionItem> 
      <DeleteIcon/>
      <EditIcon/>
     </>);
     setData(okResult);
    return okResult;
  }


    return(<>
      <AddIcon w={6} h={6} onClick={e=>AddIconClicked(e)} />
      <Accordion>{data}</Accordion>
      </>
    );
}

export default TopicList;
// {x.paragraphs.map((y:ParagraphDto)=> <AccordionPanel pb={4}>{y.name}</AccordionPanel>)}