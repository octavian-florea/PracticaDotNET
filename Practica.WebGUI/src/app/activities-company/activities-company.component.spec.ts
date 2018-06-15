import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ActivitiesCompanyComponent } from './activities-company.component';

describe('ActivitiesCompanyComponent', () => {
  let component: ActivitiesCompanyComponent;
  let fixture: ComponentFixture<ActivitiesCompanyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ActivitiesCompanyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ActivitiesCompanyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
