import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RedVoznjePrikaziLinijuComponent } from './red-voznje-prikazi-liniju.component';

describe('RedVoznjePrikaziLinijuComponent', () => {
  let component: RedVoznjePrikaziLinijuComponent;
  let fixture: ComponentFixture<RedVoznjePrikaziLinijuComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RedVoznjePrikaziLinijuComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RedVoznjePrikaziLinijuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
