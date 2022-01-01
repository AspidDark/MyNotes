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
    Textarea
  } from '@chakra-ui/react'
import React, { useState } from 'react'

export interface NoteUpdateModalUsage{
    isOpen:boolean;
    entityId:string;
    onOk:(mainEnyityId:string, head:string, body:string)=>void;
    onClose:()=>void;
    header:string;
    okMessage:string;
    cancelMessage:string;
    inputHeadLabel:string;
    inputHeadPlaceholder?:string;
    inputHeadValue?:string;
    inputBodyLabel:string;
    inputBodyPlaceholder?:string;
    inputBodyValue?:string;
}

export function NoteUpdateModal(data: NoteUpdateModalUsage){

    const [headValue, setHeadValue] = useState(data?.inputHeadValue);

    let bodyValue:string|undefined=data?.inputBodyValue;


    function headChanged(e:any){
      setHeadValue(e.target.value)
    }

    return (
        <>
        <Modal isOpen={data.isOpen} onClose={data.onClose}>
            <ModalOverlay />
            <ModalContent>
              <ModalHeader>{data.header}</ModalHeader>
              <ModalCloseButton />
              <ModalBody pb={6}>
                <FormControl mt={4}>
                  <FormLabel>{data.inputHeadLabel}</FormLabel>
                  <Textarea placeholder={data.inputHeadPlaceholder} value={headValue} onChange={headChanged} />
                  <FormLabel>{data.inputBodyLabel}</FormLabel>
                  <Textarea placeholder={data.inputBodyPlaceholder} value={bodyValue} onChange={e=>bodyValue =e.target.value} onBlur={e=>bodyValue =e.target.value} />
                </FormControl>
              </ModalBody>
    
              <ModalFooter>
                <Button colorScheme='blue' mr={3} onClick={e=> data.onOk(data.entityId, headValue as string, bodyValue as string)}>
                  {data.okMessage}
                </Button>
                <Button variant='ghost' onClick={data.onClose}>{data.cancelMessage}</Button>
              </ModalFooter>
            </ModalContent>
          </Modal>
        </>
      )
}