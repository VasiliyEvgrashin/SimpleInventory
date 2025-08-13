export class ShipmentEditModel {
  id: number = 0;
  number: string = '';
  clientid: string = '';
  issign: boolean = false;
  createdate: any = new Date();
  items: ShipmentEditItemModel[] = [];
}

export class ShipmentEditItemModel {
  id: number = 0;
  resourceid: number = 0;
  unitofmeasurementid: number = 0;
  count: number = 0;
}
