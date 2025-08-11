import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { EMPTY, map, Observable } from 'rxjs';
import { Constants } from '../../../../constants';
import { ClientsModel } from '../models/clientsmodel';
import { UnitsofmeasurementModel } from '../models/unitsofmeasurementmodel';
import { ResourcesModel } from '../models/resourcesmodel';

@Injectable()
export class GenericService {

  constructor(
    private httpClient: HttpClient
  ) {}

  getlist(mtype: string | undefined): Observable<any> {
    switch (mtype) {
      case Constants.clients_data_type_url: {
        return this
          .httpClient
          .get<any[]>(Constants.clients_data_type_url)
          .pipe(
            map((response) => response.map(item => new ClientsModel(item)))
          );
      }
      case Constants.unitsofmeasurement_data_type_url: {
        return this
          .httpClient
          .get<any[]>(Constants.unitsofmeasurement_data_type_url)
          .pipe(
            map((response) => response.map(item => new UnitsofmeasurementModel(item)))
          );
      }
      case Constants.resources_data_type_url: {
        return this
          .httpClient
          .get<any[]>(Constants.resources_data_type_url)
          .pipe(
            map((response) => response.map(item => new ResourcesModel(item)))
          );
      }
    }
    return EMPTY;
  }

  fetchOne(id: number, mtype: string): Observable<any> {
    return this.httpClient.get(
      mtype + '/' + id
    ).pipe(map((value) => {
      switch (mtype) {
        case Constants.clients_data_type_url: {
          return new ClientsModel(value);
        }
        case Constants.unitsofmeasurement_data_type_url: {
          return new UnitsofmeasurementModel(value);
        }
        case Constants.resources_data_type_url: {
          return new ResourcesModel(value);
        }
      }
      return value;
    }));
  }

  put(rm: any, mtype: string): Observable<any> {
    return this.httpClient.put(
      mtype,
      rm
    );
  }

  post(rm: any, mtype: string): Observable<any> {
    return this.httpClient.post(mtype, rm);
  }
}
