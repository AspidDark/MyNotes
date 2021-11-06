import { ChangeEvent, SyntheticEvent, useState } from 'react'
import {
    Box,
    Text,
    Flex,
    Button,
    Input,
    createStandaloneToast
} from '@chakra-ui/react'

import FileDownloadservice from '../service/fileDownloadService'
import {GetDependentEntityDto} from '../Dto/Dtos';

function FileDownload(){
    
    const [entityId, setEntityId] = useState('');
    const [mainEntityId, setmainEntityId] = useState('');

    const onEntityChange = (event: ChangeEvent<HTMLInputElement>) => {
        setEntityId(event.currentTarget.value);
    };
    const onMainEntityChange =(event: ChangeEvent<HTMLInputElement>) => {
        setmainEntityId(event.currentTarget.value);
    };

    const sendrequest=()=>{
        let dto: GetDependentEntityDto={
            entityId:entityId,
            mainEntityId:mainEntityId
        }
        const fileDownoad= new FileDownloadservice();
        fileDownoad.downloadFile(dto)
    }

    return(
        <Box
            width="50%"
            m="100px auto"
            padding="2"
            shadow="base"
        >
            <label>
            entityId:
            <Input  type="text" value={entityId}  onChange={onEntityChange} />
            </label>
            <label>
            MainEntityId:
            <Input type="text" value={mainEntityId} onChange={onMainEntityChange} />
            </label>
            <Button
                    size="sm"
                    colorScheme="green"
                    onClick={() => sendrequest()}
                >
                    Get File
                </Button>
        </Box>
    );

}

export default FileDownload