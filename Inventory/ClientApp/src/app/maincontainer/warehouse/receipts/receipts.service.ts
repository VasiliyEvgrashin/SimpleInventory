import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Constants } from "../../../constants";
import { Observable } from "rxjs";
import { ReceiptListModel } from "./receiptlistmodel";

@Injectable()
export class ReceiptsService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getlist(datefrom: any, dateto: any): Observable<ReceiptListModel[]> {
    let params = new HttpParams();
    params = params.set('datefrom', datefrom);
    params = params.set('dateto', dateto);
    return this.httpClient.get<ReceiptListModel[]>(
      Constants.receipt_url,
      { params: params }
    );
  }
}
