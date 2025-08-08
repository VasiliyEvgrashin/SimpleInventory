import { FormControl } from '@angular/forms';
import { IEditModel } from '../ieditmodel';
import { Definition } from '../definition';
import { BaseModel } from './basemodel';

export class ClientsModel extends BaseModel implements IEditModel {
  address: string | undefined;

  constructor(item: any) {
    super(item);
    this.address = item.address
  }

  getControls(): {} {
    this.controls.address = new FormControl(this.address);
    return this.controls;
  }

  getDefinition(): Definition[] {
    this.definition.push({ name: 'address', dtype: 'Input' });
    return this.definition;
  }
}
