import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PreviousPage, SweetAlertService } from '@sgpe-ws/general';
import { DetallePedido, Pedido } from '@sgpe-ws/models';
import { DetallePedidoService, PedidoService } from '@sgpe-ws/services';

@Component({
  selector: 'sgpe-ws-pedido-view',
  templateUrl: './pedido-view.component.html',
  styleUrls: ['./pedido-view.component.scss'],
})
export class PedidoViewComponent implements OnInit {
  pedido: Pedido | null = null;
  detalles: DetallePedido[] = [];
  idPedido = '';
  previousPages: PreviousPage[] = [];

  constructor(
    private pedidoService: PedidoService,
    private detalleService: DetallePedidoService,
    private route: ActivatedRoute,
    private router: Router,
    private alert: SweetAlertService) {}

  ngOnInit() {
    const currentUrl = this.router.routerState.snapshot.url;
    this.previousPages = currentUrl.includes('usuario') ?
      [{title: 'Mis Pedidos', link:'/pedidos/usuario'}] :
      [{title: 'Pedidos', link:'/pedidos'}];

    this.idPedido = this.route.snapshot.paramMap.get('id') ?? '';

    this.pedidoService.getPedido(this.idPedido).subscribe({
      next: (res) => this.pedido = res.data,
      error: (e) => this.alert.msgNormalError("Error consultando pedido", e.message)
    })

    this.detalleService.getDetallePedido(this.idPedido).subscribe(
      (res) => this.detalles = res.data
    )
  }
}
