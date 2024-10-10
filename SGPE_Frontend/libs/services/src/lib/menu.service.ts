import { Injectable } from '@angular/core';
import { environment } from '..';
import { HttpClient } from '@angular/common/http';
import { Menu, MenuUsuario, ModuloUsuario } from '@sgpe-ws/models';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { map } from 'rxjs/operators';
import { ServiceResponse } from '@sgpe-ws/general';

@Injectable({
  providedIn: 'root'
})
export class MenuService {
  private apiUrl = `${environment.API_URL}/menu`
  idMenu$ = new BehaviorSubject<string>('');
  private readonly MENU = 'MENU';

  constructor(private http: HttpClient) {}

  getMenus(id: string) {
    return this.http.get<ServiceResponse<Menu[]>>(`${this.apiUrl}/getmenusbyidmodulo/${id}`);
  }

  getMenu(idMenu: string) {
    return this.http.get<ServiceResponse<Menu>>(`${this.apiUrl}/getmenu/${idMenu}`);
  }

  getMenuUsuario() {
    return this.http.get<ServiceResponse<ModuloUsuario[]>>(`${this.apiUrl}/getmenuusuario`).pipe(
      map(response => this.gestionarModulosUsuraio(response.data))
    );
  }

  createMenu(menu: Menu) {
    return this.http.post<ServiceResponse<object>>(`${this.apiUrl}/createmenu`, menu);
  }

  updateMenu(menu: Menu) {
    return this.http.put<ServiceResponse<object>>(`${this.apiUrl}/updatemenu`, menu);
  }

  ChangeStatusMenu(idMenu: string) {
    return this.http.get<ServiceResponse<object>>(`${this.apiUrl}/changestatusmenu/${idMenu}`);
  }

  private gestionarModulosUsuraio(modulos: ModuloUsuario[]) {
    const menus: MenuUsuario[] = [];
    modulos.forEach(mod => {
      menus.push(...mod.menusUsuario);
      mod.activado = false;
      mod.menusUsuario = mod.menusUsuario.filter(option => option.orden && option.orden > 0)
    });

    this.setMenuLS(menus);

    return modulos;
  }

  urlAutorizada(url: string) {
    const optionsMenu = this.getMenuLS();

    return optionsMenu.some(f => f.url === url);;
  }

  setMenuLS(menusUsuario: MenuUsuario[]) {
    const menu = JSON.stringify(menusUsuario);
    localStorage.setItem(this.MENU, menu);
  }

  getMenuLS(): MenuUsuario[] {
    const menu = localStorage.getItem(this.MENU);
    return menu ? JSON.parse(menu) : [];
  }

}
