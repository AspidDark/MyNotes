import * as React from 'react';
import { AccordionPanel, Link } from "@chakra-ui/react";


  export interface ParagraphComponentUsage{
    mainEntityId:string;
    elementId:string;
    elementName:string;
    onParagraphClick:(mainEntityId:string, elementId:string)=>void;
  }

  export default function ParagraphComponent(paragraphData:ParagraphComponentUsage):JSX.Element{
    const pdWhat:number=4;
    const {mainEntityId, elementId, elementName } = paragraphData;


    return( <AccordionPanel onClick={e=> paragraphData.onParagraphClick(mainEntityId, elementId)} pb={pdWhat} elementId={elementId} key={elementId} >
    <Link> {elementName}</Link>
  </AccordionPanel>);
  }