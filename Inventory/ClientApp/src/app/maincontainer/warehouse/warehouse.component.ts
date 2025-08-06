import {Component, OnInit} from "@angular/core";
import { AppNavigation } from "../../navigation/appnavigation";
import { RouterOutlet } from "@angular/router";
import { SidebarComponent } from "../../sidebar/sidebar.component";

@Component({
  selector: 'app-warehouse',
  templateUrl: 'warehouse.component.html',
  styleUrls: ['warehouse.component.scss'],
  imports: [
    RouterOutlet,
    SidebarComponent
]
})
export class WarehouseComponent implements OnInit{
  public sidebarNavigationsItems: AppNavigation[];

  constructor() {
    this.sidebarNavigationsItems = [
      {
        title: 'Склад',
        routeLink: 'warehouse',
        icon: 'group',
        children: [
          {title: 'Баланс', routeLink: 'balance' },
          {title: 'Поступления', routeLink: 'receipts' },
          {title: 'Отгрузки', routeLink: 'shipments' }
        ],
        childrenOpened: false
      }
    ];
  }

  ngOnInit() {
  }
}
