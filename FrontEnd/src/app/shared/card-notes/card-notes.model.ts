
export interface ICardResponse {
    noteId: string,
    tagId: string,
    action: EnumCardAction
}

export enum EnumCardAction {
    ADD = 'ADD',
    DELETE = 'DELETE'
}