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

function Main() {
    const [data, setData]=useState<JSX.Element[]>();

    function ParametersChanged(data : NoteDto[]){
        let dataInfo:NoteDto[]=data;

        var mapedResult=dataInfo.map((x:NoteDto)=> <div> 
              <AccordionItem>
                <h2>
             <AccordionButton>
                <Box flex="0" textAlign="left" id={x.id}>
                {x.name}
                </Box>
                <AccordionIcon />
            </AccordionButton>
            </h2>
                 <AccordionPanel  pb={4} elementId={x.id} paragrtaphId={x.paragraphId}>{x.message}</AccordionPanel>
                 </AccordionItem>
        </div>);
        setData(mapedResult);
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
      <Accordion>{data}</Accordion>
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