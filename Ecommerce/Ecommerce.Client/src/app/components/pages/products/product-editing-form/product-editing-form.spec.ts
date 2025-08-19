import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductEditingForm } from './product-editing-form';

describe('ProductEditingForm', () => {
  let component: ProductEditingForm;
  let fixture: ComponentFixture<ProductEditingForm>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductEditingForm]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductEditingForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
