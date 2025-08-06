import { environment } from "../environments/environment";

export class Constants {
  constructor() {}

  static readonly clients_data_type_url: string = `${environment.apiUrl}/auth/signin`;
  static readonly unitsofmeasurement_data_type_url: string = `${environment.apiUrl}/auth/signin`;
  static readonly resources_data_type_url: string = `${environment.apiUrl}/auth/signin`;



    /* CODES FOR SIMPLE DICTIONARIES */
  static readonly clients_data_type: string = 'clients';
  static readonly unitsofmeasurement_data_type: string = 'unitsofmeasurement';
  static readonly resources_data_type: string = 'resources';
}
