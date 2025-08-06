import { CommonModule, UpperCasePipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatDrawer } from "@angular/material/sidenav";
import { RouterModule } from '@angular/router';
import { AppNavigation } from './appnavigation';

export class MGroup {
  key: string | undefined;
  items: AppNavigation[] | undefined;
}

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.scss'],
  imports: [
    CommonModule,
    UpperCasePipe,
    RouterModule,
    MatIconModule
  ]
})
export class NavigationComponent implements OnInit {
  @Input() drawer: MatDrawer | undefined;

  public title: string = '';

  public mainNavigationItems: MGroup[] = [];

  constructor() {

  }

  ngOnInit(): void {
    this.mainNavigationItems = [];
    let chits: any[] = [];
    chits.push({ title: 'Склад', routeLink: 'warehouse', icon: 'account_balance' });
    chits.push({ title: 'Справочники', routeLink: 'referencebooks', icon: 'folder_copy' });
    this.mainNavigationItems.push({
      key: 'Управелние Складом',
      items: chits
    });
  }
}
