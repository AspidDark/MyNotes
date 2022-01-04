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
    Textarea,
  } from '@chakra-ui/react'
import React,{useState} from 'react'

export interface ConfirmationModalWithInputUsage{
  isOpen:boolean;
  onOk:(vslue:string)=>void;
  onClose:()=>void;
  header:string;
  okMessage:string;
  cancelMessage:string;
  inputLabel:string;
  inputPlaceholder?:string;
  startingValue?:string;
}

//https://chakra-ui.com/docs/overlay/modal

export function ModalWithInput(data : ConfirmationModalWithInputUsage) {
  const [value, setValue]=useState('');
    return (
      <>
      <Modal isOpen={data.isOpen} onClose={data.onClose}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>{data.header}</ModalHeader>
            <ModalCloseButton />
            <ModalBody pb={6}>
              <FormControl mt={4}>
                <FormLabel>{data.inputLabel}</FormLabel>
                <Textarea placeholder={data.inputPlaceholder} onBlur={e=>setValue(e.target.value)}>{data?.startingValue}</Textarea>
              </FormControl>
            </ModalBody>
  
            <ModalFooter>
              <Button colorScheme='blue' mr={3} onClick={e=> data.onOk(value)}>
                {data.okMessage}
              </Button>
              <Button variant='ghost' onClick={data.onClose}>{data.cancelMessage}</Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </>
    )
  }