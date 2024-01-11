import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogAddTagComponent } from './dialog-add-tag.component';

describe('DialogAddTagComponent', () => {
  let component: DialogAddTagComponent;
  let fixture: ComponentFixture<DialogAddTagComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DialogAddTagComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DialogAddTagComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
