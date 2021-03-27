import { ComponentFixture, TestBed } from '@angular/core/testing';

import { YellowcardItemComponent } from './yellowcard-item.component';

describe('YellowcardItemComponent', () => {
  let component: YellowcardItemComponent;
  let fixture: ComponentFixture<YellowcardItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ YellowcardItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(YellowcardItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
