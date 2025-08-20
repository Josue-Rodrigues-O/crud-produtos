import { Pipe, PipeTransform } from '@angular/core';
import { SelectItem } from '../models/select-item';

@Pipe({
  name: 'descriptionFromList'
})
export class DescriptionFromListPipe implements PipeTransform {
  transform(value: any, list: SelectItem[]): string {
    const found = list.find(item => item.key === value);
    return found ? found.description : '';
  }
}
