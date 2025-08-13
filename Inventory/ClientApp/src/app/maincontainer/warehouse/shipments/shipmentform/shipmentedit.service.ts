import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Constants } from "../../../../constants";
import { ShipmentEditModel } from "./shipmenteditmodel";

@Injectable()
export class ShipmentEditService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getitem(id: number): Observable<ShipmentEditModel> {
    return this.httpClient.get<ShipmentEditModel>(
      Constants.shipment_url + '/' + id
    );
  }

  upsert(model: ShipmentEditModel): Observable<ShipmentEditModel> {
    return this.httpClient.post<ShipmentEditModel>(
      Constants.shipment_url,
      model
    );
  }

  delete(id: number): Observable<boolean> {
    return this.httpClient.delete<boolean>(
      Constants.shipment_url + '/' + id
    );
  }

  checkbalance(model: ShipmentEditModel): Observable<ShipmentEditModel> {
    return this.httpClient.post<ShipmentEditModel>(
      Constants.shipment_url + '/checkbalance',
      model
    );
  }
}
