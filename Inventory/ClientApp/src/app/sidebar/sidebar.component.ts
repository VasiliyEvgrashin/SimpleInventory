import { Component, HostListener, Input } from '@angular/core';
import { Router, ActivationEnd, RouterModule } from '@angular/router';
import { AppNavigation } from '../navigation/appnavigation';
import { CommonModule } from '@angular/common';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
  imports: [
    CommonModule,
    MatIconModule,
    RouterModule,
    MatTooltipModule,
    MatButtonModule
  ]
})
export class SidebarComponent {
  @Input() sidebarNavigationsItems: AppNavigation[] | undefined;
  title = '';
  public sidebarOpened = false;

  screenWidth = 1280;

  constructor(private router: Router) {
    this.router.events.subscribe((event) => {
      if (event instanceof ActivationEnd) {
        const routeTitle = event.snapshot.data['title'];
        this.title = routeTitle ? event.snapshot.data['title'] : this.title;
      }
    });
  }

  public toggleChildrenItems(title: string) {
    if (this.sidebarNavigationsItems) {
      const item = this.sidebarNavigationsItems.find(item => item.title === title);
      if (item) {
        item.childrenOpened = !item.childrenOpened;
      }
    }
  }

  sidebarSwitch() {
    this.sidebarOpened = !this.sidebarOpened;
  }

  @HostListener('window:resize', ['$event'])
  onResize() {
    this.screenWidth = window.innerWidth;
  }
}

