import { Injectable } from '@angular/core';
import { Constants } from '../../../../constants';
import { ClientsModel } from '../models/clientsmodel';
import { UnitsofmeasurementModel } from '../models/unitsofmeasurementmodel';
import { ResourcesModel } from '../models/resourcesmodel';

export class ModelS {
  name: string = '';
  model: any;
}

@Injectable()
export class GetmodelService {
  getModel(mtype: string): ModelS {
    switch (mtype) {
      case Constants.clients_data_type_url: {
        return { name: 'id', model: new ClientsModel(undefined) };
      }
      case Constants.unitsofmeasurement_data_type_url: {
        return { name: 'id', model: new UnitsofmeasurementModel(undefined) };
      }
      default: {
        return { name: 'id', model: new ResourcesModel(undefined) };
      }
    }
  }
}
