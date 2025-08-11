import { TitleCasePipe } from '@angular/common';
import { Injectable, Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'titleCaseExtended',
})
@Injectable()
export class TitleCaseExtendedPipe
  extends TitleCasePipe
  implements PipeTransform
{
  constructor() {
    super();
  }

  override transform(value: any, ...args: any[]): any {
    if (typeof value === 'string') {
      switch (value) {
        case 'name': {
          return 'Наименование';
        }
        case 'address': {
          return 'Адрес';
        }
        case 'number': {
          return 'Номер';
        }
        case 'createdate': {
          return 'Дата';
        }
        case 'items': {
          return 'Детали';
        }
        default: {
          return super.transform(value);
        }
      }
    } else {
      return null;
    }
  }
}
