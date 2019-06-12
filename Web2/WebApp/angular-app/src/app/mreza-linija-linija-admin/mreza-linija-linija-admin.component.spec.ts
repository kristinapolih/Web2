import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MrezaLinijaLinijaAdminComponent } from './mreza-linija-linija-admin.component';

describe('MrezaLinijaLinijaAdminComponent', () => {
  let component: MrezaLinijaLinijaAdminComponent;
  let fixture: ComponentFixture<MrezaLinijaLinijaAdminComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MrezaLinijaLinijaAdminComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MrezaLinijaLinijaAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
