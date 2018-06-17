import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicationsStudentComponent } from './aplications-student.component';

describe('AplicationsStudentComponent', () => {
  let component: AplicationsStudentComponent;
  let fixture: ComponentFixture<AplicationsStudentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AplicationsStudentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AplicationsStudentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
