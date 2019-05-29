import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TrenutnaLokacijaVozilaComponent } from './trenutna-lokacija-vozila.component';

describe('TrenutnaLokacijaVozilaComponent', () => {
  let component: TrenutnaLokacijaVozilaComponent;
  let fixture: ComponentFixture<TrenutnaLokacijaVozilaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TrenutnaLokacijaVozilaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TrenutnaLokacijaVozilaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
