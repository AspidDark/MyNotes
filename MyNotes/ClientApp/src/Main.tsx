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


function Main() {
    const [notesContainer, setNotesContainer]=useState<JSX.Element>();
    //const [notes, setNotes]=useState<NoteDto[]>();
    //const [isRefreshNeeded, setIsRefreshNeeded]=useState(false);
    const [refreshCount, setRefreshCount] = useState('');
    const [currentParagraph, setCurrentParagraph] = useState('');

    useEffect(() => {
        ParametersChanged(currentParagraph);
      }, [refreshCount]);


    function addNote(e:any, paragraphId:string ) {
        let noteName:string = e.target.value;
        let api= new NoteApi();
        let noteToAdd: AddNoteDto={
            name:noteName,
            message:"",
            paragraphId

        };
        api.postNote(noteToAdd);
        setRefreshCount(Guid.newGuid());
       // setIsRefreshNeeded(!isRefreshNeeded);
    }

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
        if(notesResult && notesResult.result)
        {
            let noteData:NoteDto[]=notesResult.data  as NoteDto[];
            if(noteData && noteData.length>0){
                setNotesContainer(<>
                    <Input placeholder="Note:" onBlur={e=>addNote(e, paragraphId)} />
                   {NotesArray(noteData) }
                   </>);
           // setIsRefreshNeeded(!isRefreshNeeded); 
                   return;
            }
        }
        setNotesContainer(<Input placeholder="Note:" onBlur={e=>addNote(e, paragraphId)} />);
        //setIsRefreshNeeded(!isRefreshNeeded); 
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
            //setNotes(notesResult.data as NoteDto[]);
            return notesResult.data as NoteDto[];
        }
        //setNotes(undefined);
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

class Guid {
    static newGuid() {
      return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random() * 16 | 0,
          v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
      });
    }
  }


//<TopicList />


// <TopicComponent />
//            <FileUpload />
//<FileDownload />
//<ParagraphComponent />
export default Main