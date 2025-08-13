import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Constants } from "../../../constants";
import { Observable } from "rxjs";
import { BalanceModel } from "./balancemodel";

@Injectable()
export class BalanceService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getlist(resources: number[], units: number[]): Observable<BalanceModel[]> {
    return this.httpClient.post<BalanceModel[]>(
      Constants.balance_url,
      {resources: resources, units: units}
    );
  }
}
