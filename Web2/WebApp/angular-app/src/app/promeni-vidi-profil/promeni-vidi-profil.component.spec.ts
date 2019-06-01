import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PromeniVidiProfilComponent } from './promeni-vidi-profil.component';

describe('PromeniVidiProfilComponent', () => {
  let component: PromeniVidiProfilComponent;
  let fixture: ComponentFixture<PromeniVidiProfilComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PromeniVidiProfilComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PromeniVidiProfilComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
