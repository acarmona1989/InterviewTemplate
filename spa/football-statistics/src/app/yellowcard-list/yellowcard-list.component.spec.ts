import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YellowcardListComponent } from './yellowcard-list.component';

describe('YellowcardListComponent', () => {
  let component: YellowcardListComponent;
  let fixture: ComponentFixture<YellowcardListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YellowcardListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(YellowcardListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
