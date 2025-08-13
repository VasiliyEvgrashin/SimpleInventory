import { environment } from "../environments/environment";

export const MY_DATE_FORMATS = {
  parse: {
    dateInput: 'DD.MM.YYYY',
  },
  display: {
    dateInput: 'DD.MM.YYYY',
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

export class Constants {
  constructor() {}
  static readonly NVARCHAR_MAX: number = 100;

  static readonly clients_data_type_url: string = `${environment.apiUrl}/Client`;
  static readonly unitsofmeasurement_data_type_url: string = `${environment.apiUrl}/UnitOfMeasurement`;
  static readonly resources_data_type_url: string = `${environment.apiUrl}/Resource`;

  static readonly receipt_url: string = `${environment.apiUrl}/Receipt`;
  static readonly shipment_url: string = `${environment.apiUrl}/Shipment`;
}
