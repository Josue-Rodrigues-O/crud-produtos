import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'priceLabel'
})
export class PriceLabelPipe implements PipeTransform {
    transform(value: number): string {
        return value ? value.toFixed(2) : '0.00';
    }
}
