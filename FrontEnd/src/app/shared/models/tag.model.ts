export interface ITag {
    id?: string,
    noteId: string,
    name: string,
    bgColor: ITagColor ,
    color: ITagColor
}

export interface ITagColor {
    alpha?: number,
    red: number,
    green: number,
    blue: number
}