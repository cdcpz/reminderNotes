import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogService, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';

@Component({
  selector: 'app-dialog-register',
  standalone: true,
  providers: [],
  imports: [
    ReactiveFormsModule,
    FormsModule,
    DynamicDialogModule,
    InputTextModule,
    ButtonModule,
    InputTextareaModule,
  ],
  templateUrl: './dialog-register.component.html',
  styleUrl: './dialog-register.component.scss'
})
export class DialogRegisterComponent implements OnInit {
  forms!: FormGroup

  constructor(
    private form: FormBuilder,
    private ref: DynamicDialogRef
  ) {

  }
  ngOnInit(): void {
    this.forms = this.form.group({
      id: [''],
      title: [''],
      body: ['']
    })
  }

  register() {
    this.ref.close(this.forms.value)
  }

  closeDialog() {
    this.ref.close()
  }
}