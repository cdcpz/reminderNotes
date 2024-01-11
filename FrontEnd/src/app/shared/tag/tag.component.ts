import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { TagModule } from 'primeng/tag';
import { EnumCardAction, ICardResponse } from '../card-notes/card-notes.model';
import { ITag } from '../models/tag.model';
import { NgStyle } from '@angular/common';

@Component({
  selector: 'app-tag',
  standalone: true,
  imports: [TagModule, NgStyle],
  templateUrl: './tag.component.html',
  styleUrl: './tag.component.scss'
})
export class TagComponent {

  @Input ({required: true}) tag: ITag = {} as ITag
  @Output() choosed = new EventEmitter<ICardResponse>()

  constructor() {}

  get id() {
    return this.tag.id ?? 'N/A'
  }

  get name() {
    return this.tag.name
  }

  get tagColor() {
    return {
      'border': `1px solid rgba(${this.color.red},${this.color.green},${this.color.blue},${this.color.alpha})`,
      'background-color': `rgba(${this.bgColor.red},${this.bgColor.green},${this.bgColor.blue},0.3)`,
      'color': 'black'
    }
  }

  get color() {
    return this.tag.color
  }

  get bgColor() {
    return this.tag.bgColor
  }

  deleteTag(){
    this.choosed.emit({
      action: EnumCardAction.DELETE,
      noteId:'',
      tagId: this.tag.id!,
    })
  }
}
