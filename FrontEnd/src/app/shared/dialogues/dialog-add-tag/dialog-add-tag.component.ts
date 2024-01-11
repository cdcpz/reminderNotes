import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DynamicDialogRef } from 'primeng/dynamicdialog';
import { InputTextModule } from 'primeng/inputtext';
import {ColorPickerModule} from 'primeng/colorpicker';

@Component({
  selector: 'app-dialog-add-tag',
  standalone: true,
  imports: [
    ButtonModule,
    InputTextModule,
    FormsModule,
    ReactiveFormsModule,
    ColorPickerModule
  ],
  templateUrl: './dialog-add-tag.component.html',
  styleUrl: './dialog-add-tag.component.scss'
})
export class DialogAddTagComponent implements OnInit {
  forms!: FormGroup

  /**
   *
   */
  constructor(
    private form: FormBuilder,
    private ref: DynamicDialogRef
  ) {
  }

  ngOnInit(): void {

    this.forms = this.form.group({
      tag: [''],
      color: [{red: 0, green: 0, blue: 0}]
    })
  }

  register() {
    this.ref.close(this.forms.value)
  }


}
