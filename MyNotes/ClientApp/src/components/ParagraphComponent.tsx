import * as React from 'react';
import { AccordionPanel, Link, IconButton } from "@chakra-ui/react";
import { DeleteIcon, EditIcon } from '@chakra-ui/icons'

  export interface ParagraphComponentUsage{
    mainEntityId:string;
    elementId:string;
    elementName:string;
    onParagraphClick:(mainEntityId:string, elementId:string)=>void;
    Update:(id:string, value:string)=>void;
    Delete:(id:string)=>void;
  }

  export default function ParagraphComponent(paragraphData:ParagraphComponentUsage):JSX.Element{
    const pdWhat:number=4;
    const {mainEntityId, elementId, elementName } = paragraphData;

    return( <AccordionPanel onClick={e=> paragraphData.onParagraphClick(mainEntityId, elementId)} pb={pdWhat} elementId={elementId} key={elementId} >
    <Link> {elementName}</Link>
    <IconButton 
        aria-label="Edit Paragraph"
        size="sm"
        icon={<EditIcon />} 
        onClick={e=> paragraphData.Update(elementId, elementName)}
      />
       <IconButton 
        aria-label="Delete Paragraph"
        size="sm"
        icon={<DeleteIcon />} 
        onClick={e=> paragraphData.Delete(elementId)}
      />

  
  </AccordionPanel>);
  }