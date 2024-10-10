import { Component } from '@angular/core';
import { AuthService } from '@sgpe-ws/services';

@Component({
  selector: 'sgpe-ws-top-menu',
  templateUrl: './top-menu.component.html',
  styles: [`
  a {
    cursor: pointer;
  }
  `]
})

export class TopMenuComponent {
  constructor(public authService: AuthService) {}

}
