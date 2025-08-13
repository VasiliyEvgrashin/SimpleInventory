export class ShipmentListModel {
  id: number = 0;
  number: string = '';
  createdate: any;
  client: string = '';
  issign: boolean = false;
  items: ShipmentListItemModel[] = [];
}

export class ShipmentListItemModel {
  name: string = '';
  uofm: string = '';
  count: number = 0;
}
