import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RedVoznjeAdminComponent } from './red-voznje-admin.component';

describe('RedVoznjeAdminComponent', () => {
  let component: RedVoznjeAdminComponent;
  let fixture: ComponentFixture<RedVoznjeAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RedVoznjeAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RedVoznjeAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
