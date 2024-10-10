import { Component } from '@angular/core';

@Component({
  selector: 'sgpe-ws-main-layout',
  template: `
  <sgpe-ws-layout>
    <router-outlet></router-outlet>
  </sgpe-ws-layout>`,
})
export class MainLayoutComponent {}
