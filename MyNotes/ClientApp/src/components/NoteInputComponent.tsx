import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    ModalCloseButton,
    Button,
    FormControl,
    FormLabel,
    Input,
  } from '@chakra-ui/react'
import React,{useState} from 'react'

export interface NoteInputUsage{
    mainEntityId:string;
    headPlaceholder:string;
    headValue:string;
    bodyPlaceholder:string;
    bodyValue:string;
    onOk:(paragraphId:string, head:string, body:string)=>void;
    onClose:()=>void;
}

export function NoteInputComponent(data:NoteInputUsage){
    const [currentHead, setCurrentHead]=useState('');
    const [currentBody, setCurrentBody]=useState('');

    setCurrentHead(data.headValue);
    setCurrentBody(data.bodyValue);

    return(<>
        <Input placeholder={data.headPlaceholder} value={currentHead} onBlur={e=>setCurrentHead(e.target.value)} />
        <Input placeholder={data.bodyPlaceholder} value={currentBody} onBlur={e=>setCurrentBody(e.target.value)} />
        <Button colorScheme='green' onClick={()=>data.onOk(data.mainEntityId, currentHead, currentBody)}>Ok</Button>
        <Button colorScheme='blue' onClick={data.onClose}>Cancel</Button>
    </>);

}
//e.target.value