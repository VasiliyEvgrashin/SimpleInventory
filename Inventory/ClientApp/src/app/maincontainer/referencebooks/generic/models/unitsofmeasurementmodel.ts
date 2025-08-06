import { IEditModel } from '../ieditmodel';
import { Definition } from '../definition';
import { BaseModel } from './basemodel';

export class UnitsofmeasurementModel extends BaseModel implements IEditModel {

  getControls(): {} {
    return this.controls;
  }

  getDefinition(): Definition[] {
    return this.definition;
  }
}
