import { Route } from '@angular/router';
import { IndexComponent } from '../pages/index/index.component';
import { UnauthorizedComponent } from '../components/unauthorized/unauthorized.component';

export const routes: Route[] = [
  {
    path: '',
    pathMatch: 'full',
    component: IndexComponent,
    title: 'Inicio'
  },
  {
    path: 'noautorizado',
    pathMatch: 'full',
    component: UnauthorizedComponent,
    title: 'No autorizado'
  },
  {
    path: 'productos',
    loadChildren: () =>
      import('@sgpe-ws/productos').then((m) => m.ProductosModule)
  },
  {
    path: 'empresas',
    loadChildren: () =>
      import('@sgpe-ws/empresas').then((m) => m.EmpresasModule)
  },
  {
    path: 'categoria-productos',
    loadChildren: () =>
      import('@sgpe-ws/categoria-productos').then((m) => m.CategoriaProductosModule)
  },
  {
    path: 'usuarios',
    loadChildren: () =>
      import('@sgpe-ws/usuarios').then((m) => m.UsuariosModule)
  },
  {
    path: 'modulos',
    loadChildren: () =>
      import('@sgpe-ws/modulos').then((m) => m.ModulosModule)
  },
  {
    path: 'perfiles',
    loadChildren: () =>
      import('@sgpe-ws/perfiles').then((m) => m.PerfilesModule)
  },
  {
    path: 'pedidos',
    loadChildren: () =>
      import('@sgpe-ws/pedidos').then((m) => m.PedidosModule)
  }
];
