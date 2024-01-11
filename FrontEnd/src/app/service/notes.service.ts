import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { IApiResponse } from '../shared/models/response';
import { environment } from '../../environments/environment';
import { INote } from '../shared/models/models';
import { ITag } from '../shared/models/tag.model';

@Injectable({
  providedIn: 'root'
})
export class NotesService {

  constructor(private readonly http: HttpClient) { }


  // filter(){
  //   return this.http.get<IApiResponse<INote[]>>(`${environment.apiUrl}Note/Get?track=${filter}`)
  // }

  saveNote(note: INote){
   return this.http.post<IApiResponse<INote>>(`${environment.apiUrl}Note/Add`,note)
  }
  getAll(filter: string = ''){
    return this.http.get<IApiResponse<INote[]>>(`${environment.apiUrl}Note/Get?track=${filter}`)
  }

  deleteNote(noteId:string){
    return this.http.delete<IApiResponse<INote[]>>(`${environment.apiUrl}Note/Delete/${noteId}`)
  }
  saveTag(tag: ITag){
   return this.http.post<IApiResponse<ITag>>(`${environment.apiUrl}Tag/Add`,{
    ...tag,
    tag: tag.name
   })
  }
  deleteTag(data: any){
    return this.http.delete<IApiResponse<ITag>>(`${environment.apiUrl}Tag/Delete/${data.noteId}/${data.tagId}`)
  }
}
