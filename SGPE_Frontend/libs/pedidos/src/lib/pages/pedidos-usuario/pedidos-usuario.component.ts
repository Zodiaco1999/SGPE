import { Component, OnInit } from '@angular/core';
import { Pedido } from '@sgpe-ws/models';
import { PedidoUsuarioService } from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-pedidos-usuario',
  templateUrl: './pedidos-usuario.component.html',
  styleUrls: ['./pedidos-usuario.component.scss'],
})
export class PedidosUsuarioComponent implements OnInit {
  data$: Observable<Pedido[]>;
  totalRecords$: Observable<number>;

  constructor(
    public pedidoService: PedidoUsuarioService
  ) {
    this.data$ = pedidoService.data$;
    this.totalRecords$ = pedidoService.totalRecords$;
  }

  ngOnInit() {
    this.pedidoService.Search();
  }
}
