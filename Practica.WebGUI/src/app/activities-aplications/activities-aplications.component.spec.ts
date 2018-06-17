import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivitiesAplicationsComponent } from './activities-aplications.component';

describe('ActivitiesAplicationsComponent', () => {
  let component: ActivitiesAplicationsComponent;
  let fixture: ComponentFixture<ActivitiesAplicationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActivitiesAplicationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivitiesAplicationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
