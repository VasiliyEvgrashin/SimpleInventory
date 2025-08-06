import { Component } from "@angular/core";
import { MatIconModule } from "@angular/material/icon";
import { MatMenuModule } from "@angular/material/menu";
import { MatSidenavModule } from "@angular/material/sidenav";
import { Router, RouterOutlet } from "@angular/router";
import { NavigationComponent } from "../navigation/navigation.component";
import { MatButtonModule } from "@angular/material/button";
import { MatDialog } from "@angular/material/dialog";

@Component({
  selector: 'maincontainer',
  templateUrl: 'maincontainer.component.html',
  styleUrl: 'maincontainer.component.scss',
  imports: [
    MatSidenavModule,
    RouterOutlet,
    MatIconModule,
    MatMenuModule,
    NavigationComponent,
    MatButtonModule
  ]
})

export class MainContainerComponent {


  constructor(
    public dialog: MatDialog
  ) { }


}
