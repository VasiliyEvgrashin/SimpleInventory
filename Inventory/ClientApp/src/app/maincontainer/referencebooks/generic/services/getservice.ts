import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EMPTY, Observable } from 'rxjs';
import { Constants } from '../../../../constants';
import { ClientsModel } from '../models/clientsmodel';
import { UnitsofmeasurementModel } from '../models/unitsofmeasurementmodel';
import { ResourcesModel } from '../models/resourcesmodel';

@Injectable()
export class GetService {

  constructor(
    private httpClient: HttpClient
  ) {}

  getlist(mtype: string | undefined): Observable<any> {
    switch (mtype) {
      case Constants.clients_data_type: {
        return this.httpClient.get<ClientsModel[]>(Constants.clients_data_type_url);
      }
      case Constants.unitsofmeasurement_data_type: {
        return this.httpClient.get<UnitsofmeasurementModel[]>(Constants.unitsofmeasurement_data_type_url);
      }
      case Constants.resources_data_type: {
        return this.httpClient.get<ResourcesModel[]>(Constants.resources_data_type_url);
      }
    }
    return EMPTY;
  }
}
