import { Component, OnInit, inject } from '@angular/core';
import { ToolbarModule } from 'primeng/toolbar';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CardNotesComponent } from '../../shared/card-notes/card-notes.component';
import { NotesService } from '../../service/notes.service';
import { INote } from '../../shared/models/models';
import { DialogRegisterComponent } from '../../shared/dialogues/dialog-register/dialog-register.component';
import { DialogAddTagComponent } from '../../shared/dialogues/dialog-add-tag/dialog-add-tag.component';
import { EnumCardAction, ICardResponse } from '../../shared/card-notes/card-notes.model';
import { DialogConfirmationComponent } from '../../shared/dialogues/dialog-confirmation/dialog-confirmation.component';
import { ITag, ITagColor } from '../../shared/models/tag.model';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    ToolbarModule,
    ButtonModule,
    CardNotesComponent,
    DialogRegisterComponent,
    ReactiveFormsModule,
    FormsModule,

  ],
  providers: [NotesService, DialogService, DynamicDialogRef],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {

  notes: INote[] = []
  ref: DynamicDialogRef | undefined;
  forms!: FormGroup
  constructor(
    private readonly _notes: NotesService,
    public dialogService: DialogService,
    private readonly form: FormBuilder
  ) {

  }

  ngOnInit(): void {
    this.getNotes()
    this.forms = this.form.group({
      filter: ['']
    })
  }

  filter() {
    this.getNotes(this.forms.value.filter)
  }

  getNotes(filter: string = '') {
    this._notes.getAll(filter).subscribe(resp => {
      this.notes = []
      this.notes = resp.data
    })
  }

  saveNote(notes: INote) {
    this._notes.saveNote(notes).subscribe(resp => {
      this.getNotes()
    })
  }

  showDialogRegister() {
    this.ref = this.dialogService.open(DialogRegisterComponent, {
      header: 'Add note',
    });

    this.ref.onClose.subscribe((resp) => {
      if (!resp) return
      this.saveNote(resp)
    });
  }

  manageCardResponse(response: ICardResponse) {
    if (response.action == EnumCardAction.ADD) {
      this.showDialogAddTag(response.noteId)
    }
    if (response.action == EnumCardAction.DELETE) {
      if (response.tagId) {
        this.deleteTag(response)
        return
      }
      this.showDeleteNote(response.noteId)
    }
  }

  showDialogAddTag(id: string) {
    this.ref = this.dialogService.open(DialogAddTagComponent, {
      header: 'Tag note',
      width: '250px',
    });

    this.ref.onClose.subscribe((resp) => {
      if (!resp) return
      let tag: ITag = {
        noteId: id,
        name: resp.tag,
        bgColor:{} as ITagColor,
        color: {
          red: resp.color.red,
          green: resp.color.green,
          blue: resp.color.blue
        }
      }
      this._notes.saveTag(tag).subscribe(resp => {

        this.getNotes()
      })
    });
  }

  showDeleteNote(id: string) {
    this.ref = this.dialogService.open(DialogConfirmationComponent, {
      header: 'Attention',
      width: 'auto',
      data: 'Are you sure you want to delete this note?'
      // contentStyle: { overflow: 'auto' },
      // baseZIndex: 10000,
      // maximizable: true
    });

    this.ref.onClose.subscribe((resp) => {
      if (!resp) return
      this._notes.deleteNote(id).subscribe(resp => {
        this.getNotes()
      })

    });
  }
  deleteTag(data: ICardResponse) {
    let dataId = {
      tagId: data.tagId,
      noteId: data.noteId
    }

    this._notes.deleteTag(dataId).subscribe(resp => {
      this.getNotes()
    })
  }
}
