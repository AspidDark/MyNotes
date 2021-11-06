export interface AddEntityDto{
    id:string
}

export interface BaseBodyDto{
    id:string
    createDate:Date,
    editDate:Date,
    ownerId:string
}

export interface GetDependentEntityDto {
    entityId: string,
    mainEntityId:string
}

export interface BaseDto<T>{
    result:boolean,
    data:T
}
