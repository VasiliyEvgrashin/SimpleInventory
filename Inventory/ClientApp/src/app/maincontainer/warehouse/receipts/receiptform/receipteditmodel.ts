export class ReceiptEditModel {
  id: number = 0;
  number: string = '';
  createdate: any = new Date();
  items: ReceiptEditItemModel[] = [];
}

export class ReceiptEditItemModel {
  id: number = 0;
  resourceid: number = 0;
  unitofmeasurementid: number = 0;
  count: number = 0;
}
