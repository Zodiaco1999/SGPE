import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { pedidosRoutes } from './lib.routes';
import { GeneralModule } from '@sgpe-ws/general';
import { PedidoEditComponent } from './pages/pedido-edit/pedido-edit.component';
import { PedidosUsuarioComponent } from './pages/pedidos-usuario/pedidos-usuario.component';
import { PedidoViewComponent } from './pages/pedido-view/pedido-view.component';
import { PedidosComponent } from './pages/pedidos/pedidos.component';
import { PedidoPreviewComponent } from './components/pedido-preview/pedido-preview.component';

@NgModule({
  imports: [CommonModule, GeneralModule, RouterModule.forChild(pedidosRoutes)],
  declarations: [
    PedidoEditComponent,
    PedidosUsuarioComponent,
    PedidoViewComponent,
    PedidosComponent,
    PedidoPreviewComponent,
  ],
})
export class PedidosModule {}
