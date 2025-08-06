import {Component, OnInit} from "@angular/core";
import { AppNavigation } from "../../navigation/appnavigation";
import { RouterOutlet } from "@angular/router";
import { SidebarComponent } from "../../sidebar/sidebar.component";

@Component({
  selector: 'app-referencebooks',
  templateUrl: 'referencebooks.component.html',
  styleUrls: ['referencebooks.component.scss'],
  imports: [
    RouterOutlet,
    SidebarComponent
]
})
export class ReferencebooksComponent implements OnInit{
  public sidebarNavigationsItems: AppNavigation[];

  constructor() {
    this.sidebarNavigationsItems = [
      {
        title: 'Справочники',
        routeLink: 'referencebooks',
        icon: 'group',
        children: [
          {title: 'Клиенты', routeLink: 'clients' },
          {title: 'Единицы Измерения', routeLink: 'unitsofmeasurement' },
          {title: 'Ресурсы', routeLink: 'resources' }
        ],
        childrenOpened: false
      }
    ];
  }

  ngOnInit() {
  }
}
