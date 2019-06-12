import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DodajKontroleraAdminComponent } from './dodaj-kontrolera-admin.component';

describe('DodajKontroleraAdminComponent', () => {
  let component: DodajKontroleraAdminComponent;
  let fixture: ComponentFixture<DodajKontroleraAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DodajKontroleraAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DodajKontroleraAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
