import { Component, OnInit } from '@angular/core';
import { MenuService, ModuloService } from '@sgpe-ws/services';
import { SweetAlertService } from '@sgpe-ws/general'
import { ActivatedRoute } from '@angular/router';
import { Menu, Modulo } from '@sgpe-ws/models';

@Component({
  selector: 'sgpe-ws-modulo-view',
  templateUrl: './modulo-view.component.html',
  styleUrls: ['./modulo-view.component.scss'],
})
export class ModuloViewComponent implements OnInit {
  modulo: Modulo = {};
  idModulo = '';
  menus: Menu[] = [];

  constructor(
    private moduloService: ModuloService,
    public menuService: MenuService,
    private alert: SweetAlertService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.idModulo = this.route.snapshot.paramMap.get('id') ?? '';

    this.moduloService.getModulo(this.idModulo)
      .subscribe({
        next: (response) => this.modulo = response.data,
        error: (e) => this.alert.msgNormalError('Modulo inexistente', e.message)
      });

    this.getMenus();
  }

  setIdMenu(idMenu: string) {
    this.menuService.idMenu$.next(idMenu);
  }

  getMenus() {
    this.menuService.getMenus(this.idModulo)
      .subscribe({
        next: (response) => this.menus = response.data
      });
  }

  async changeStatusMenu(idMenu: string, activo: boolean) {
    const change = await this.alert.msgComfirm('Cambiar estado', `¿Seguro que quiere ${activo ? 'inactivar' : 'activar'} el menú?`)

    if (change) {
      this.menuService.ChangeStatusMenu(idMenu)
      .subscribe({
        next: (res) => {
          this.alert.msgSimpleSuccess(res.message);
          this.getMenus();
        },
        error: (e) => this.alert.msgNormalError('No se logro cambiar estado', e.message)
      });
    }
  }
}
