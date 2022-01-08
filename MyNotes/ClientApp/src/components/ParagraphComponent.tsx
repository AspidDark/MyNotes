import * as React from 'react';
import { AccordionPanel, Link, IconButton, Grid, GridItem } from "@chakra-ui/react";
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
      <Grid templateColumns='repeat(3, 1fr)' gap={2}>
        <GridItem w='100%'>
          <Link> {elementName}</Link>
        </GridItem>
        <GridItem w='100%'> 
          <IconButton 
              aria-label="Edit Paragraph"
              size="sm"
              icon={<EditIcon />} 
              onClick={e=> paragraphData.Update(elementId, elementName)}
          />
        </GridItem>
        <GridItem w='100%'>
            <IconButton 
              aria-label="Delete Paragraph"
              size="sm"
              icon={<DeleteIcon />} 
              onClick={e=> paragraphData.Delete(elementId)}
            />
        </GridItem>
      </Grid>
  </AccordionPanel>);
  }