import { ITag } from "./tag.model";

export interface INote {
    id: string,
    title: string,
    body: string,
    tags: ITag,
    
}
// export const INIT_NOTES: INote[] = [
//     {
//         id: "",
//         title: "",
//         body: ""
//     }
// ]