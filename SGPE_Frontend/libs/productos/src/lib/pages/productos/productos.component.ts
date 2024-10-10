import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from '@sgpe-ws/general';
import { EstadoProducto, Producto } from '@sgpe-ws/models';
import { ProductoService } from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-productos',
  templateUrl: './productos.component.html',
  styleUrls: ['./productos.component.scss'],
})
export class ProductosComponent implements OnInit {
  data$: Observable<Producto[]>;
  totalRecords$: Observable<number>;

  constructor(
    public productoService: ProductoService,
    private alert: SweetAlertService
  ) {
    this.data$ = productoService.data$;
    this.totalRecords$ = productoService.totalRecords$;
  }

  estadoProductos: EstadoProducto[] = [
    {
      idEstadoProducto: 1,
      descripcionEstadoProducto: 'Activo',
    },
    {
      idEstadoProducto: 2,
      descripcionEstadoProducto: 'Inactivo',
    },
    {
      idEstadoProducto: 3,
      descripcionEstadoProducto: 'Agotado',
    },
  ];

  ngOnInit() {
    this.getProducts();
  }

  async changeStatus(producto: Producto, idEstadoProducto: number) {
    if (producto.idEstadoProducto != idEstadoProducto) {
      const change = await this.alert.msgComfirm('Cambio de estado',
      `¿Quiere asignarle al producto ${producto.descripcionProducto}, el estado:${this.estadoProductos.find(p=> p.idEstadoProducto == idEstadoProducto)?.descripcionEstadoProducto}?`);

      if (change) {
        this.productoService
          .ChangeStatusProducto(producto.idProducto, idEstadoProducto)
          .subscribe((response) => {
            if (response.success) {
              this.alert.msgSimpleSuccess(response.message);
              this.getProducts();
            }
          });
      }
    }
  }

  getProducts() {
    this.productoService.Search();
  }
}
