import {Button, Textarea} from '@chakra-ui/react'
import React from 'react'

export interface NoteInputUsage{
    mainEntityId:string;
    headPlaceholder:string;
    headValue:string;
    bodyPlaceholder:string;
    bodyValue:string;
    onOk:(paragraphId:string, head:string, body:string)=>void;
    onClose:()=>void;
}

export function NoteInputComponent(usageData:NoteInputUsage):JSX.Element{
    let currentHead:string=usageData.headValue;
    let currentBody:string=usageData.bodyValue;

    return(<>
        <Textarea placeholder={usageData.headPlaceholder} onBlur={e=>currentHead=e.target.value} />
        <Textarea placeholder={usageData.bodyPlaceholder} onBlur={e=>currentBody=e.target.value} />
        <Button colorScheme='green' onClick={()=>usageData.onOk(usageData.mainEntityId, currentHead, currentBody)}>Ok</Button>
        <Button colorScheme='blue' onClick={usageData.onClose}>Cancel</Button>
    </>);

}
//e.target.value