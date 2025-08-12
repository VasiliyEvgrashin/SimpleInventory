import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ReceiptEditModel } from "./receipteditmodel";
import { Constants } from "../../../../constants";

@Injectable()
export class ReceiptEditService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getitem(id: number): Observable<ReceiptEditModel> {
    return this.httpClient.get<ReceiptEditModel>(
      Constants.receipt_url + '/' + id
    );
  }

  upsert(model: ReceiptEditModel): Observable<ReceiptEditModel> {
    return this.httpClient.post<ReceiptEditModel>(
      Constants.receipt_url,
      model
    );
  }

  delete(id: number): Observable<boolean> {
    return this.httpClient.delete<boolean>(
      Constants.receipt_url + '/' + id
    );
  }
}
