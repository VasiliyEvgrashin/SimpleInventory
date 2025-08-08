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
        default: {
          return super.transform(value);
        }
      }
    } else {
      return null;
    }
  }
}
