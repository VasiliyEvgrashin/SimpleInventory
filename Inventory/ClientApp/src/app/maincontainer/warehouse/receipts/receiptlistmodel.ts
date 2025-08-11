export class ReceiptListModel {
  id: number = 0;
  number: string = '';
  createdate: any;
  items: ReceiptListItemModel[] = [];
}

export class ReceiptListItemModel {
  name: string = '';
  uofm: string = '';
  count: number = 0;
}
