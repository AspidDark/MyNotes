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
import { AddTopicDto, UpdateTopicDto} from "../Dto/TopicDto";

import { IconButton } from "@chakra-ui/react"

import {BaseModal} from "../components/common/BaseModal"
import { ConfirmationModal } from "../components/common/Modals/confirmation-modal"
import { useModal } from "../components/common/useModal";



function TopicList(dataFunc:DataFunction){
  
  const [data, setData]=useState<JSX.Element[]>();
  const [isLoaded, setLoaded]=useState(false);
  const [isRefreshNeeded, setIsRefreshNeeded]=useState(false);
  const [isAddClicked, setIsAddClicked]=useState(false);
  const [newTopicName, setNewTopicName]=useState('');
  const [deleteTopicId, setDeleteTopicId] =useState('');

  const { isShown, toggle } = useModal();
  
  //https://nainacodes.com/blog/create-an-accessible-and-reusable-react-modal
  useEffect(() => {
    renderListAsync();
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
      console.log(result.data);
      return;
    }
    dataFunc.dataFunc(result.data as NoteDto[]);
  }
  
  //create
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
  

  
  async function AddIconClicked(event:any) {
    if(isAddClicked)
    {
      return;      
    }
    setIsAddClicked(true);
    setData([SetInput(),...data as JSX.Element[]]);
  }
  
  async function deleteTopicClick(event:any, entityId:string) {
    setDeleteTopicId(entityId);
    toggle();
    //Модалку подтверждалку
  }

  async function onDeleteConfirm () {
    const requestService=new TopicApi();
    const result = await requestService.deleteTopic(deleteTopicId);
          if(!result.result){
              let qqq=55;
              return;
          }
        let dataResult=result.data as string;
        let qqq2=66;
    setIsRefreshNeeded(!isRefreshNeeded); 
    toggle();
  }
  const onDeletCancel = ()=>{
    setDeleteTopicId('');
    toggle();
  }


  function SetInput():JSX.Element {
    return ( <Input placeholder="small size" size="sm" onBlur={e=>CeateTopic(e.target.value)}/>);
  }

  function CreateInputField(entityId:string):JSX.Element {
    return ( <Input placeholder="small size" size="sm" onBlur={e=>EditTopic(e.target.value, entityId)}/>);
  }


   let editTopicClick = (event:any, entityId:string) =>{
    let newData:JSX.Element[]=[];
    data?.forEach(x => {
      if(x.props.children.key!=entityId){
        newData.push(x);
      }
      else{
        newData.push(CreateInputField(x.props.children.key))
      }
    });
    if(newData.length>0){
      setData(newData);
    }
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
      <IconButton 
        aria-label="Edit Topic"
        size="sm"
        icon={<EditIcon />} 
        onClick={e=> editTopicClick(e, x.id)}
      />
       <IconButton 
        aria-label="Delete Topic"
        size="sm"
        icon={<DeleteIcon />} 
        onClick={e=> deleteTopicClick(e, x.id)}
      />
    </h2>
      {x.paragraphs.map((y:ParagraphDto)=> 
        <AccordionPanel onClick={e=> paragraphClicked(e, x.id, y.id)} pb={4} elementId={y.id} key={y.id} >
          <Link> {y.name}</Link>
        </AccordionPanel>
        )}
     </AccordionItem>
        
     <BaseModal
        isShown={isShown}
        hide={toggle}
        headerText="Confirmation"
        modalContent={
          <ConfirmationModal
            onConfirm={onDeleteConfirm}
            onCancel={onDeletCancel}
            message="Are you sure you want to delete element?"
          />
        }
      />

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
      </>
    );
}

export default TopicList;
// {x.paragraphs.map((y:ParagraphDto)=> <AccordionPanel pb={4}>{y.name}</AccordionPanel>)}