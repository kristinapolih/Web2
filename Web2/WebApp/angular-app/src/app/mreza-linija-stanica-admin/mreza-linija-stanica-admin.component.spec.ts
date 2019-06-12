import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MrezaLinijaStanicaAdminComponent } from './mreza-linija-stanica-admin.component';

describe('MrezaLinijaStanicaAdminComponent', () => {
  let component: MrezaLinijaStanicaAdminComponent;
  let fixture: ComponentFixture<MrezaLinijaStanicaAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MrezaLinijaStanicaAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MrezaLinijaStanicaAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
