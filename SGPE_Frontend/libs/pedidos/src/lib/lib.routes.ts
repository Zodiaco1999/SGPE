import { Route } from '@angular/router';
import { PedidoEditComponent } from './pages/pedido-edit/pedido-edit.component';
import { PedidosUsuarioComponent } from './pages/pedidos-usuario/pedidos-usuario.component';
import { PedidoViewComponent } from './pages/pedido-view/pedido-view.component';
import { PedidosComponent } from './pages/pedidos/pedidos.component';

export const pedidosRoutes: Route[] = [
  { path: '', pathMatch: 'full', component: PedidosComponent, title: 'Pedidos' },
  { path: 'crear', pathMatch: 'full', component: PedidoEditComponent, title: 'Realizar pedido' },
  { path: 'editar/:id', pathMatch: 'full', component: PedidoEditComponent, title: 'Editar pedido' },
  { path: 'detalle/:id', pathMatch: 'full', component: PedidoViewComponent, title: 'Detalle pedido' },
  { path: 'usuario', pathMatch: 'full', component: PedidosUsuarioComponent, title: 'Mis pedidos' },
  { path: 'usuario/detalle/:id', pathMatch: 'full', component: PedidoViewComponent, title: 'Detalle pedido' }
];
