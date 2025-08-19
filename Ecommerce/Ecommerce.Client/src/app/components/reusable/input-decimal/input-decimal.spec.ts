import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InputDecimal } from './input-decimal';

describe('InputDecimal', () => {
  let component: InputDecimal;
  let fixture: ComponentFixture<InputDecimal>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [InputDecimal]
    })
    .compileComponents();

    fixture = TestBed.createComponent(InputDecimal);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
