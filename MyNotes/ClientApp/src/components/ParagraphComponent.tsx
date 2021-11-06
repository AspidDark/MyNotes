import { ChangeEvent, SyntheticEvent, useState } from 'react'
import {
    Box,
    Text,
    Flex,
    Button,
    Input,
    createStandaloneToast
} from '@chakra-ui/react'
import ParagraphApi from '../Apis/paragraphApi'
import {ParagraphDto, AddParagraphDto} from '../Dto/ParagraphDto'
import {BaseDto, AddEntityDto } from '../Dto/Dtos';

function ParagraphComponent(){

    const [message, setMessage] = useState('');
   
    async function sendGetRequest(){
        const requeatService=new ParagraphApi();
        //4e6b9346-21f8-45ef-997e-08c86c500919
        const result= await requeatService.getParagraph("4e6b9346-21f8-45ef-997e-08c86c500909");
        if(!result.result){
            onSetMessage(result.data as string)
            let qqq=1;
            console.log(result.data);
            return;
        }
        let dataResult=result.data as ParagraphDto;
        console.log(dataResult);
        let qqq2=2;
    }

    async function sendGetListRequest(){
        const requeatService=new ParagraphApi();
        const result= await requeatService.getParagraphs({pageNumber:0, pageSize:3, mainEntityId:'4e6b9346-21f8-45ef-997e-08c86c500909'});
        if(!result.result){
            onSetMessage(result.data as string)
            let qqq=3;
            return;
        }
        let dataResult=result.data as ParagraphDto[];
        let qqq2=4;
    }

    async function sendPostRequest(){
        let addEntity:AddParagraphDto={
            name:"Posted from React",
            topicId:'4e6b9346-21f8-45ef-997e-08c86c500909'
        }
        const requeatService=new ParagraphApi();
        const result = await requeatService.postParagraph(addEntity);
        if(!result.result){
            onSetMessage(result.data as string)
            let qqq=5;
            return;
        }
        let dataResult=result.data as AddEntityDto;
        let qqq2=6;
    }

    async function sendDeleteRequest(){
        let entityId:string='6470230c-0c2a-4078-b5ff-d404cd6179ad';
        const requeatService=new ParagraphApi();
        const result = await requeatService.deleteParagraph(entityId);
        if(!result.result){
            onSetMessage(result.data as string)
            let qqq=7;
            return;
        }
        let dataResult=result.data as string;
        let qqq2=8;
    }

    async function sendPutRequest(){
        let entityId:string='6470230c-0c2a-4078-b5ff-d404cd6179ad';
        let name='From React PUT Thi beste';
        const requeatService=new ParagraphApi();
        const result = await requeatService.updateParagraph({name:name, id:entityId});
        if(!result.result){
            let qqq=9;
        }
        let dataResult=result.data as ParagraphDto;
        let qqq2=10;
    }

    const onSetMessage =(message:string):void => {
        setMessage(message);
    };

    return(
        <Box
            width="50%"
            m="100px auto"
            padding="2"
            shadow="base"
        >
            <Button
                    size="sm"
                    colorScheme="green"
                    onClick={() => sendGetRequest()}
                >
                    Get Api Call
                </Button>

                <Button
                    size="sm"
                    colorScheme="green"
                    onClick={() => sendGetListRequest()}
                >
                    Get List Api Call
                </Button>

                <Button
                    size="sm"
                    colorScheme="blue"
                    onClick={() => sendPostRequest()}
                >
                    Post
                </Button>

                <Button
                    size="sm"
                    colorScheme="yellow"
                    onClick={() => sendDeleteRequest()}
                >
                    Delete
                </Button>

                <Button
                    size="sm"
                    colorScheme="orange"
                    onClick={() => sendPutRequest()}
                >
                    Update
                </Button>

                <label>
                Message:
                <Input  type="text" value={message} />
                </label>

        </Box>
    );
}

export default ParagraphComponent