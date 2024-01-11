import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MenuItem, MenuItemCommandEvent } from 'primeng/api';
import { CardModule } from 'primeng/card';
import { MenuModule } from 'primeng/menu';
import { ToastModule } from 'primeng/toast';
import { TagModule } from 'primeng/tag';
import { TagComponent } from '../tag/tag.component';
import { NgIf } from '@angular/common';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { EnumCardAction, ICardResponse } from './card-notes.model';
import { ITag } from '../models/tag.model';

@Component({
  selector: 'app-card-notes',
  standalone: true,
  imports: [
    CardModule,
    MenuModule,
    TagComponent,
    NgIf
  ],
  templateUrl: './card-notes.component.html',
  styleUrl: './card-notes.component.scss'
})
export class CardNotesComponent implements OnInit {

  @Input() title: string = ""
  @Input() body: string = ""
  @Input() id: string = ""
  @Input() tag: any = {} as ITag
  @Output() choosed = new EventEmitter<ICardResponse>()

  items: MenuItem[] = []
  tags: any[] = []
  visibleTag: boolean = false;

  ref: DynamicDialogRef | undefined;
  constructor(
    public dialogService: DialogService
  ) {

  }

  ngOnInit(): void {
    this.itemsMenu()
    this.tags = this.tag
    
  }

  itemsMenu(){
    this.items = [

      {
        label: 'Add tag',
        command: () => {
          this.choosed.emit({
            noteId: this.id,
            tagId: this.tag.id,
            action: EnumCardAction.ADD
          })
        }
      },
      {
        label: 'Delete note',
        command: () => {
          this.choosed.emit({
            tagId:'',
            noteId: this.id,
            action: EnumCardAction.DELETE
          })
        }
      }
    ];
  }

  manageTagResponse(response: ICardResponse) {
    if (response.action == EnumCardAction.DELETE) {

      this.choosed.emit({
        action: EnumCardAction.DELETE,
        noteId: this.id,
        tagId: response.tagId

      })
    }
  }
}
