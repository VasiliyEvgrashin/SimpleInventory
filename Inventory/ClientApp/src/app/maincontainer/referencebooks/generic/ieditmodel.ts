import { Definition } from "./definition";

export interface IEditModel {
  getControls(service: any): {};
  getDefinition(): Definition[];
}
