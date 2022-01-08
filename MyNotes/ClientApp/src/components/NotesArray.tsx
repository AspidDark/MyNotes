import { AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    IconButton,
    Grid,
    GridItem,
    Box} from '@chakra-ui/react' 
import React from 'react';
import { DeleteIcon, EditIcon } from '@chakra-ui/icons'

import { NoteDto } from "../Dto/NotesDtos";

function NotesArray(data : NoteDto[], updateNoteFunc:(x:NoteDto)=>void, deleteNoteFunc:(x:NoteDto)=>void):JSX.Element[]{

    var mapedResult=data.map((x:NoteDto)=> <> 
        <AccordionItem>
           <h2>
           <Grid templateColumns='repeat(3, 1fr)' gap={2}>
                <GridItem w='100%'>
                    <AccordionButton>
                        <Box flex="0" textAlign="right" id={x.id}>
                            {x.name}
                        </Box>
                        <AccordionIcon />
                    </AccordionButton>
                </GridItem>
                <GridItem w='100%'>   
                    <IconButton 
                        aria-label="Edit Topic"
                        size="sm"
                        icon={<EditIcon />} 
                        onClick={e=>updateNoteFunc(x)}
                    />
                </GridItem>
                <GridItem w='100%'>
                    <IconButton 
                        aria-label="Delete Topic"
                        size="sm"
                        icon={<DeleteIcon />}
                        onClick={e=>deleteNoteFunc(x)} 
                    />
                </GridItem>
            </Grid>
            </h2>
        <AccordionPanel  pb={4} elementId={x.id} paragrtaphId={x.paragraphId}>{x.message} </AccordionPanel>
        </AccordionItem>
    </>);
    return mapedResult;
}
//https://chakra-ui.com/docs/layout/flex

export default NotesArray;