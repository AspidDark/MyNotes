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
    let headValue:string|undefined=data.inputHeadValue;
    let bodyValue:string|undefined=data.inputBodyValue;



    let updateIfChanged=(entityId:string,
      oldHead:string|undefined,
      newHead:string|undefined,
      oldBody:string|undefined,
      newBody:string|undefined,
      onOk:(mainEnyityId:string, head:string, body:string)=>void,
      onClose:()=>void
      )=>{
        if(oldHead===newHead&&oldBody===newBody){
          onClose();
          return;
        }
        onOk(entityId, newHead as string, newBody as string);
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
                  <Textarea placeholder={data.inputHeadPlaceholder} onChange={e=>headValue =e.target.value} onBlur={e=>headValue =e.target.value}>{data.inputHeadValue}</Textarea>
                  <FormLabel>{data.inputBodyLabel}</FormLabel>
                  <Textarea placeholder={data.inputBodyPlaceholder} onChange={e=>bodyValue =e.target.value} onBlur={e=>bodyValue =e.target.value}>{data.inputBodyValue}</Textarea>
                </FormControl>
              </ModalBody>
    
              <ModalFooter>
                <Button colorScheme='blue' mr={3} onClick={e=> updateIfChanged(data.entityId, data.inputHeadValue, headValue, data.inputBodyValue, bodyValue, data.onOk, data.onClose)}>
                  {data.okMessage}
                </Button>
                <Button variant='ghost' onClick={data.onClose}>{data.cancelMessage}</Button>
              </ModalFooter>
            </ModalContent>
          </Modal>
        </>
      )
}