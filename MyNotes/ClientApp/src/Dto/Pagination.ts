export interface Pagination{
    pageNumber:number,
    pageSize:number
}

export interface PaginatonWithMainEntity extends Pagination{
    mainEntityId:string
}