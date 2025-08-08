import { IEditModel } from '../ieditmodel';
import { Definition } from '../definition';
import { BaseModel } from './basemodel';

export class ResourcesModel extends BaseModel implements IEditModel {

  constructor(item: any) {
    super(item);
  }

  getControls(): {} {
    return this.controls;
  }

  getDefinition(): Definition[] {
    return this.definition;
  }
}
