import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProveriKarteComponent } from './proveri-karte.component';

describe('ProveriKarteComponent', () => {
  let component: ProveriKarteComponent;
  let fixture: ComponentFixture<ProveriKarteComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProveriKarteComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProveriKarteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
