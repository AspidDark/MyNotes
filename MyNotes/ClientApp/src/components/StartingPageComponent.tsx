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
    Input, 
    Button,
    useDisclosure
  } from "@chakra-ui/react";

import { DeleteIcon, AddIcon, EditIcon } from '@chakra-ui/icons'
import React, { ChangeEvent, ReactComponentElement, SyntheticEvent, useEffect, useState } from 'react'
import TopicApi from '../Apis/topicApi'
import {StartingPageDto} from '../Dto/startingPageDto'
import StartingPageApi from '../Apis/startingPageApi'
import {AddEntityDto, BaseDto} from '../Dto/Dtos';
import {ParagraphDto} from '../Dto/ParagraphDto'
import NoteApi from "../Apis/notesApi";
import ParagraphApi from "../Apis/paragraphApi";
import { NoteDto } from "../Dto/NotesDtos";
import {PaginatonWithMainEntity} from '../Dto/Pagination';
import DataFunction from './InternalTypes/MainWindowTextData';
import { AddTopicDto, UpdateTopicDto} from "../Dto/TopicDto";

import { IconButton } from "@chakra-ui/react"

import {ConfirmationModal} from "../components/common/Modals/ConfirmationModal";
import {ModalWithInput} from "../components/common/Modals/ModalWithInput";
import { forEach } from "lodash";

function TopicList(dataFunc:DataFunction){
  
  const [data, setData]=useState<JSX.Element[]>();
  const [newParagraph, setNewParagraph]=useState<ParagraphShower[]>();

  const [isLoaded, setLoaded]=useState(false);
  const [isRefreshNeeded, setIsRefreshNeeded]=useState(false);
  const [isAddClicked, setIsAddClicked]=useState(false);
  const [deleteTopicId, setDeleteTopicId] =useState('');
  const [updateTopicId, setUpdateTopicId] =useState('');

  const { isOpen, onOpen, onClose } = useDisclosure();
  const { isOpen:isUpdateOpen, onOpen:onUpdateOpen, onClose:onUpdateClose }  = useDisclosure();

  useEffect(() => {
    renderListAsync(setNewParagraph);
  }, [isRefreshNeeded]);
  
  async function paragraphClicked(event:any, mainEntityId:string, entityId:string){
    const paginated : PaginatonWithMainEntity=
    {
      mainEntityId:entityId,
      pageNumber:0,
      pageSize:20
    }
    const noteApi= new NoteApi();
    const result = await noteApi.getNotes(paginated);
    
    if(!result.result){
      //Error hadle
      let empty:NoteDto[]=[];
      dataFunc.dataFunc(empty);
      return;
    }
    dataFunc.dataFunc(result.data as NoteDto[]);
  }
  
  //create
  async function AddIconClicked(event:any) {
   if(isAddClicked){
      return;      
    }
  setIsAddClicked(true);
  setData([SetInput(),...data as JSX.Element[]]);
}

function SetInput():JSX.Element {
  return ( <Input placeholder="small size" size="sm" onBlur={e=>CeateTopic(e.target.value)}/>);
}

  async function CeateTopic(value:string) {
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
    setIsRefreshNeeded(!isRefreshNeeded); 
    setIsAddClicked(false);
  }

  //edit
  let editTopicClick = (event:any, entityId:string, name:string) =>{
    setUpdateTopicId(entityId);
    onUpdateOpen();
  }

  async function onUpdateConfirm(newName:string) {
    onUpdateClose();
    await EditTopic(newName, updateTopicId);
  }

  const onUpdateCancel =()=>{
    setUpdateTopicId('');
    onUpdateClose();
  }

  async function EditTopic(value:string, entityId:string) {
    if( value )
    {
      const requestService=new TopicApi();
      let updateDto:UpdateTopicDto={
        name:value,
        topicId:entityId
      }
      const result = await requestService.updateTopic(updateDto);
      if(!result.result){
        let qqq=55;
        return;
      }
      let dataResult=result.data as AddEntityDto;
      let qqq2=66;
    }
    setIsRefreshNeeded(!isRefreshNeeded); 
  }
  
  //delete
  async function deleteTopicClick(event:any, entityId:string) {
    setDeleteTopicId(entityId);
    onOpen();
  }

  async function onDeleteConfirm () {
    onClose();
    const requestService=new TopicApi();
    const result = await requestService.deleteTopic(deleteTopicId);
          if(!result.result){
              let qqq=55;
              return;
          }
        let dataResult=result.data as string;
        let qqq2=66;
    setIsRefreshNeeded(!isRefreshNeeded);

  }
  const onDeletCancel = ()=>{
    setDeleteTopicId('');
    onClose();
  }
//Add Paragraph
async function addParagraphClick(event:any, 
  topicId:string, 
  paragaphSetter:(newParagraps:ParagraphShower[])=>void,
  paragraphsIds:string[]
   ) {
  let newParagraps:ParagraphShower[]=[];
  for(let i = 0; i<paragraphsIds.length; i++)
  {
    if(paragraphsIds[i]==topicId){
      let showInput:ParagraphShower={
        isShow:true,
        topicId:topicId
      };
      newParagraps.push(showInput);
      continue;
    }
    let showInput:ParagraphShower={
      isShow:false,
      topicId:paragraphsIds[i]
    };
    newParagraps.push(showInput);
  }
  paragaphSetter(newParagraps);
  setIsRefreshNeeded(!isRefreshNeeded);
}

const isAddingNewParagraph=( topicId:string, paragraps?:ParagraphShower[]): boolean => {
  if(!paragraps){
    return false;
  }
  for(let i = 0; i<paragraps.length; i++){
      if(paragraps[i].topicId==topicId){
        return paragraps[i].isShow;
      }
  }
  return false;
}

async function CreateParagraph(e:any, name:string, topicId:string) {
    e.target.value="";
    const paragraphApi= new ParagraphApi();
    const result = await paragraphApi.postParagraph({name, topicId});
    if(!result.result){
      let qqq=55;
      return;
    }
    if(!newParagraph){
      return;
    }
    hideAllParagrphInputs();
    setIsRefreshNeeded(!isRefreshNeeded);
}

const hideAllParagrphInputs =()=>{
  if(newParagraph){
    let hidenParagraphs:ParagraphShower[]=[];
    for(let i = 0; i<newParagraph.length; i++){
      const hidenParagraph:ParagraphShower={
        isShow:false,
        topicId:newParagraph[i].topicId
      }
      hidenParagraphs.push(hidenParagraph)
    }
    setNewParagraph(hidenParagraphs);
  } 
}


  async function renderListAsync(paragaphSetter:(newParagraps:ParagraphShower[])=>void) {
    const requeatService=new StartingPageApi();
    const result = await requeatService.getStartingData();
    if(!result.result){
      setLoaded(false);
      return result.data as string;
    }
    let dataResult=result.data as StartingPageDto[];

    let paragraphIds:string[]=dataResult.map(x=>x.id);
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
      <IconButton 
        aria-label="Edit Topic"
        size="sm"
        icon={<EditIcon />} 
        onClick={e=> editTopicClick(e, x.id, x.name)}
      />
       <IconButton 
        aria-label="Delete Topic"
        size="sm"
        icon={<DeleteIcon />} 
        onClick={e=> deleteTopicClick(e, x.id)}
      />
      <IconButton 
        aria-label="Add Paragraph"
        size="sm"
        icon={<AddIcon />} 
        onClick={e=> addParagraphClick(e, x.id, paragaphSetter, paragraphIds)}
      />
    </h2>
      <Input hidden={!isAddingNewParagraph(x.id, newParagraph)} onBlur={e=>CreateParagraph(e, e.target.value, x.id)} />
      {x.paragraphs.map((y:ParagraphDto)=> 
        <AccordionPanel onClick={e=> paragraphClicked(e, x.id, y.id)} pb={4} elementId={y.id} key={y.id} >
          <Link> {y.name}</Link>
        </AccordionPanel>
        )}
     </AccordionItem>
     </>);
     setData(okResult);
  }

    return(<>

      <IconButton 
      aria-label="Add Topic"
      size="sm"
      icon={<AddIcon />} 
      onClick={e=>AddIconClicked(e)} />
      <Accordion>{data}</Accordion>

      <ConfirmationModal 
      isOpen={isOpen} 
      onOk={onDeleteConfirm} 
      onClose={onDeletCancel} 
      header="Delete Conformation" 
      body="Topc will be deleted" 
      okMessage="Ok" 
      cancelMessage="Cancel" />

      <ModalWithInput
      isOpen={isUpdateOpen}
      onOk={onUpdateConfirm}
      onClose={onUpdateCancel}
      header="Update Confirmation"
      okMessage="Ok" 
      cancelMessage="Cancel"
      inputLabel="New Topic Name"
      />

      </>
    );
}

export default TopicList;

interface ParagraphShower{
  isShow:boolean,
  topicId:string
}
// {x.paragraphs.map((y:ParagraphDto)=> <AccordionPanel pb={4}>{y.name}</AccordionPanel>)}