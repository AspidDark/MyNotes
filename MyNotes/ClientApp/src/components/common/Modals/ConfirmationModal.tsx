import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    ModalCloseButton,
    useDisclosure,
    Button,
  } from '@chakra-ui/react'
import React from 'react'

export interface ConfirmationModalUsage{
    isOpen:boolean;
    onOk:()=>void;
    onClose:()=>void;
    header:string;
    body:string;
    okMessage:string;
    cancelMessage:string;
}


export function ConfirmationModal(data:ConfirmationModalUsage) {
    return (
      <>
        <Modal isOpen={data.isOpen} onClose={data.onClose}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>{data.header}</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
                {data.body}
            </ModalBody>
  
            <ModalFooter>
              <Button colorScheme='red' mr={3} onClick={data.onOk}>
                {data.okMessage}
              </Button>
              <Button variant='ghost' onClick={data.onClose}>{data.cancelMessage}</Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
      </>
    )
  }
