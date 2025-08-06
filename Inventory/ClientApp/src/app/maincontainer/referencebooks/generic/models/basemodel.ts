import { FormControl, Validators } from '@angular/forms';
import { Definition } from '../definition';

export class BaseModel {
  id: number | undefined;
  name: string | undefined;

  controls: any;
  definition: Definition[] = []

  constructor() {
    this.controls = {
      name: new FormControl(this.name, Validators.required)
    };
    this.definition.push({ name: 'name', dtype: 'Input' });
  }
}
