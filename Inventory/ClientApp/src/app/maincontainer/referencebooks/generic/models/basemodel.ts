import { FormControl, Validators } from '@angular/forms';
import { Definition } from '../definition';
import { Constants } from '../../../../constants';

export class BaseModel {
  id: number = 0;
  name: string = '';

  controls: any;
  definition: Definition[] = []

  constructor(item: any) {
    if (item) {
      this.id = item.id;
      this.name = item.name;
    }
    this.controls = {
      name: new FormControl(this.name, [Validators.required, Validators.maxLength(Constants.NVARCHAR_MAX)])
    };
    this.definition.push({ name: 'name', dtype: 'Input' });
  }
}
