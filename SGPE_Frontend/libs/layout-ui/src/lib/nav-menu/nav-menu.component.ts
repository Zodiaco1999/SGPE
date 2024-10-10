import { Component, OnInit } from '@angular/core';
import { ModuloUsuario } from '@sgpe-ws/models';
import { AuthService, MenuService } from '@sgpe-ws/services';

@Component({
  selector: 'sgpe-ws-nav-menu',
  styles: [`
    has-arrow-botom {
      transform: rotate(220deg);
    }
  `],
  templateUrl: './nav-menu.component.html'
})
export class NavMenuComponent implements OnInit {
  modulos: ModuloUsuario[] = [];

  constructor(
    private menuService: MenuService,
    private authService: AuthService) {}

  ngOnInit() {
    if (this.authService.isLoggedIn()) {
      this.menuService.getMenuUsuario().subscribe((res) => {
        this.modulos = res;
      });
    }
  }

  toggleModulo(idModulo: string) {
    const mod = this.modulos.find(m => m.idModulo === idModulo);
    if (mod) {
      mod.activado = !mod.activado;
    }

  }
}
