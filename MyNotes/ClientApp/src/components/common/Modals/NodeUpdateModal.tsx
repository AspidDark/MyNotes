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