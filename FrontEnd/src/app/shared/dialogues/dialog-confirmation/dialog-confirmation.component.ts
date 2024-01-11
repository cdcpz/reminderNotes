import { Component, Input } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DynamicDialogConfig, DynamicDialogModule, DynamicDialogRef } from 'primeng/dynamicdialog';

@Component({
  selector: 'app-dialog-confirmation',
  standalone: true,
  imports: [
    ButtonModule,
    DynamicDialogModule
  ],
  templateUrl: './dialog-confirmation.component.html',
  styleUrl: './dialog-confirmation.component.scss'
})
export class DialogConfirmationComponent {

  constructor(
    private ref: DynamicDialogRef,
    public config: DynamicDialogConfig
  ) {

  }

  delete() {
    this.ref.close(true)
  }

  close() {
    this.ref.close()
  }

}
