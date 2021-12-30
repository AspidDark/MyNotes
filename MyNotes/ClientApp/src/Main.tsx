import { ChakraProvider, SimpleGrid, Textarea,  Accordion,
    AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    Grid,
    GridItem,
    Box, Text, Input} from '@chakra-ui/react' 

//import FileUpload from './Components/FileUpload'
//import FileDownload from './Components/FileDownload'
//import TopicComponent from './Components/TopicComponent'
//import ParagraphComponent from './Components/ParagraphComponent'
import TopicList from './components/StartingPageComponent'
import { topicId } from './Consts/TempConsts'
//import TempAAA from './Components/AAATempComponent'
//import  DataFunction from './Components/InternalTypes/MainWindowTextData'
import React, { useState, useEffect } from 'react';
import { AddNoteDto, NoteDto } from "./Dto/NotesDtos";

import { IconButton } from "@chakra-ui/react"
import { DeleteIcon, AddIcon, EditIcon } from '@chakra-ui/icons'

import NotesArray from "./components/NotesArray";
import NoteApi from './Apis/notesApi';
import { PaginatonWithMainEntity } from './Dto/Pagination';
import { Guid } from './service/Guid';
import {NoteInputComponent, NoteInputUsage} from './components/NoteInputComponent';


function Main() {
    const [notesContainer, setNotesContainer]=useState<JSX.Element>();
    const [refresh, setRefresh] = useState('');
    const [currentParagraph, setCurrentParagraph] = useState('');

    useEffect(() => {
        ParametersChanged(currentParagraph);
      }, [refresh]);

    const refreshComponent =() =>setRefresh(Guid.newGuid());

    async function addNote(paragraphId:string, noteName:string, body:string ) {
        let api= new NoteApi();
        let noteToAdd: AddNoteDto={
            name:noteName,
            message:body,
            paragraphId

        };
        await api.postNote(noteToAdd);
        refreshComponent();
    }

    const cancelAddingNote =()=>setRefresh(Guid.newGuid());

    async function ParametersChanged(paragraphId:string){
        if(!paragraphId){
            return  (<><Text fontSize='6xl'>Select Topic</Text></>);
        }
        setCurrentParagraph(paragraphId);
        let dataInfo:NoteDto[]|undefined=await getNotes(paragraphId);

        if(dataInfo&&dataInfo.length>0){
            setNotesContainer(<>
            <IconButton 
             aria-label="Add Topic"
             size="sm"
             icon={<AddIcon />} 
            onClick={e=>AddNoteClicked(e, paragraphId)} />
            {NotesArray(dataInfo) }
            </>);
        return;
        }
        setNotesContainer(<>
         <IconButton 
             aria-label="Add Topic"
             size="sm"
             icon={<AddIcon />} 
            onClick={e=>AddNoteClicked(e, paragraphId)} />
        <Text fontSize='6xl'>No Note</Text></>);
    }

   async function AddNoteClicked(event:any, paragraphId:string){
        let api= new NoteApi();
        const noteRequest:PaginatonWithMainEntity={
            mainEntityId:paragraphId,
            pageNumber:0,
            pageSize:30
        }
        let notesResult = await api.getNotes(noteRequest);

        let noteInputUsage:NoteInputUsage={
            mainEntityId:paragraphId,
            headPlaceholder:"Note Header",
            headValue:"",
            bodyPlaceholder:"Note Body",
            bodyValue:"",
            onOk:addNote,
            onClose: cancelAddingNote
        }

        if(notesResult && notesResult.result)
        {
            let noteData:NoteDto[]=notesResult.data  as NoteDto[];
            if(noteData && noteData.length>0){
               

                setNotesContainer(<>
                    {NoteInputComponent(noteInputUsage)}
                   {NotesArray(noteData) }
                   </>);
                   return;
            }
        }
        setNotesContainer(NoteInputComponent(noteInputUsage));
    }

    async function getNotes(paragraphId:string):Promise<NoteDto[]|undefined> {
        let api= new NoteApi();
        const noteRequest:PaginatonWithMainEntity={
            mainEntityId:paragraphId,
            pageNumber:0,
            pageSize:30
        }
        let notesResult = await api.getNotes(noteRequest);
        if(notesResult && notesResult.result){
            return notesResult.data as NoteDto[];
        }
        return undefined;
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