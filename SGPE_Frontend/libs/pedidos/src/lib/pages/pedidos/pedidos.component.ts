import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from '@sgpe-ws/general';
import { Pedido } from '@sgpe-ws/models';
import { PedidoService } from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-pedidos',
  templateUrl: './pedidos.component.html',
  styleUrls: ['./pedidos.component.scss'],
})
export class PedidosComponent implements OnInit {
  data$: Observable<Pedido[]>;
  totalRecords$: Observable<number>;

  constructor(
    public pedidoService: PedidoService,
    private alert: SweetAlertService
  ) {
    this.data$ = pedidoService.data$;
    this.totalRecords$ = pedidoService.totalRecords$;
  }

  ngOnInit() {
    this.pedidoService.Search();
  }
}
