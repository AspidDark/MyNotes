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

export interface ConfirmationModalWithInputUsage{
  isOpen:boolean;
  onOk:(vslue:string)=>void;
  onClose:()=>void;
  header:string;
  okMessage:string;
  cancelMessage:string;
  inputLabel:string;
  inputPlaceholder?:string
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
                <Input placeholder={data.inputPlaceholder} onBlur={e=>setValue(e.target.value)} />
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