import { ChakraProvider, SimpleGrid, Textarea,  Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Grid,
    GridItem,
    Box, Text} from '@chakra-ui/react' 

//import FileUpload from './Components/FileUpload'
//import FileDownload from './Components/FileDownload'
//import TopicComponent from './Components/TopicComponent'
//import ParagraphComponent from './Components/ParagraphComponent'
import TopicList from './components/StartingPageComponent'
import { topicId } from './Consts/TempConsts'
//import TempAAA from './Components/AAATempComponent'
//import  DataFunction from './Components/InternalTypes/MainWindowTextData'
import React, { useState } from 'react';
import { NoteDto } from "./Dto/NotesDtos";

import { IconButton } from "@chakra-ui/react"
import { DeleteIcon, AddIcon, EditIcon } from '@chakra-ui/icons'

import NotesArray from "./components/NotesArray";


function Main() {
    const [notesContainer, setNotesContainer]=useState<JSX.Element>();
    const [notes, setNotes]=useState<NoteDto[]>();

    function ParametersChanged(data : NoteDto[]){
        let dataInfo:NoteDto[]=data;

        if(dataInfo&&dataInfo.length>0)
        {
            setNotes(data);
            setNotesContainer(<>
         <IconButton 
      aria-label="Add Topic"
      size="sm"
      icon={<AddIcon />} 
      onClick={e=>AddNoteClicked(e)} />
        {NotesArray(dataInfo) }
        </>);
        return;
        }
        setNotesContainer(<>
         <IconButton 
             aria-label="Add Topic"
             size="sm"
             icon={<AddIcon />} 
            onClick={e=>AddNoteClicked(e)} />
        <Text fontSize='6xl'>No Note</Text></>);
    }

    function AddNoteClicked(event:any){
        if(notes && notes.length>0){
           
        }

    }

    return (
        <ChakraProvider>
            <Grid 
            templateRows="repeat(1, 1fr)"
            templateColumns="repeat(10, 1fr)">
                <GridItem rowSpan={2} colSpan={1}>
            <Box> <TopicList dataFunc={ParametersChanged} /> </Box>
            </GridItem>
            <GridItem colSpan={9} >
            <Box>
     <>
      <Accordion>{notesContainer}</Accordion>
      </></Box>
      </GridItem>
            </Grid>
        </ChakraProvider>
    )
}



//<TopicList />


// <TopicComponent />
//            <FileUpload />
//<FileDownload />
//<ParagraphComponent />
export default Main