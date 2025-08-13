import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Constants } from "../../../constants";
import { ShipmentListModel } from "./shipmentlistmodel";

@Injectable()
export class ShipmentsService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getlist(datefrom: any, dateto: any): Observable<ShipmentListModel[]> {
    let params = new HttpParams();
    params = params.set('datefrom', datefrom);
    params = params.set('dateto', dateto);
    return this.httpClient.get<ShipmentListModel[]>(
      Constants.shipment_url,
      { params: params }
    );
  }
}
