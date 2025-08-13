import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Constants } from "../constants";

@Injectable()
export class CheckUniqService {

  constructor(
    private httpClient: HttpClient
  ) { }

  checkReceipt(value: string): Observable<boolean> {
    return this.httpClient.get<boolean>(
      Constants.checkunic_url + '/receipt?value=' + value
    );
  }

  checkShipment(value: string): Observable<boolean> {
    return this.httpClient.get<boolean>(
      Constants.checkunic_url + '/shipment?value=' + value
    );
  }
}
