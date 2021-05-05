import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ExLibComponent } from './ex-lib.component';

describe('ExLibComponent', () => {
  let component: ExLibComponent;
  let fixture: ComponentFixture<ExLibComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ExLibComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ExLibComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
