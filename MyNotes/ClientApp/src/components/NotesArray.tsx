import { AccordionItem,
    AccordionButton,
    AccordionPanel,
    AccordionIcon,
    IconButton,
    Box} from '@chakra-ui/react' 
import React, { useState } from 'react';
import { DeleteIcon, AddIcon, EditIcon } from '@chakra-ui/icons'

import { NoteDto } from "../Dto/NotesDtos";

function NotesArray(data : NoteDto[]):JSX.Element[]{
    var mapedResult=data.map((x:NoteDto)=> <> 
                 <AccordionItem>
                   <h2>
                <AccordionButton>
                     <Box flex="0" textAlign="right" id={x.id}>
                     {x.name}
                        </Box>
                    <AccordionIcon />
                </AccordionButton>
                <IconButton 
                aria-label="Edit Topic"
                size="sm"
                 icon={<EditIcon />} 
                />
                 <IconButton 
                 aria-label="Delete Topic"
                    size="sm"
                    icon={<DeleteIcon />} 
                />
                <IconButton 
                 aria-label="Edit Text"
                    size="sm"
                 icon={<EditIcon />} 
                />
                    </h2>
                 <AccordionPanel  pb={4} elementId={x.id} paragrtaphId={x.paragraphId}>{x.message} </AccordionPanel>
                 </AccordionItem>
        </>);
        return mapedResult;
}

export default NotesArray;