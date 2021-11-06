import { ChangeEvent, SyntheticEvent, useState } from 'react'
import {
    Box,
    Text,
    Flex,
    Button,
    Input,
    createStandaloneToast
} from '@chakra-ui/react'
import TopicApi from '../Apis/topicApi'
import {TopicDto, AddTopicDto} from '../Dto/TopicDto';
import {BaseDto, AddEntityDto } from '../Dto/Dtos';

function TopicComponent(){
    
    const [message, setMessage] = useState('');
   
    async function sendGetRequest(){
        const requeatService=new TopicApi();
        //4e6b9346-21f8-45ef-997e-08c86c500919
        const result= await requeatService.getTopic("4e6b9346-21f8-45ef-997e-08c86c500909");
        if(!result.result){
            onSetMessage(result.data as string)
            let qqq=1;
            console.log(result.data);
            return;
        }
        let dataResult=result.data as TopicDto;
        console.log(dataResult);
        let qqq2=2;
    }

    async function sendGetListRequest(){
        const requeatService=new TopicApi();
        const result= await requeatService.getTopics({pageNumber:0, pageSize:3});
        if(!result.result){
            onSetMessage(result.data as string)
            let qqq=3;
            return;
        }
        let dataResult=result.data as TopicDto[];
        let qqq2=4;
    }

    async function sendPostRequest(){
        let addTopicDto:AddTopicDto={
            name:"Posted from React",
        }
        const requeatService=new TopicApi();
        const result = await requeatService.postTopic(addTopicDto);
        if(!result.result){
            onSetMessage(result.data as string)
            let qqq=5;
            return;
        }
        let dataResult=result.data as AddEntityDto;
        let qqq2=6;
    }

    async function sendDeleteRequest(){
        let topicId:string='f96eb72e-94f2-4749-8d86-ccc0d926f6e1';
        const requeatService=new TopicApi();
        const result = await requeatService.deleteTopic(topicId);
        if(!result.result){
            onSetMessage(result.data as string)
            let qqq=7;
            return;
        }
        let dataResult=result.data as string;
        let qqq2=8;
    }

    async function sendPutRequest(){
        let topicId:string='f96eb72e-94f2-4749-8d86-ccc0d926f6e1';
        let name='From React PUT Thi beste';
        const requeatService=new TopicApi();
        const result = await requeatService.updateTopic({name:name, topicId:topicId});
        if(!result.result){
            let qqq=9;
        }
        let dataResult=result.data as TopicDto;
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

export default TopicComponent