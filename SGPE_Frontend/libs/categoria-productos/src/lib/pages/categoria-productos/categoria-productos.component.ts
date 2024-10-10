import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from '@sgpe-ws/general';
import { CategoriaProducto } from '@sgpe-ws/models';
import { CategoriaProductoService } from '@sgpe-ws/services'
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-categoria-productos',
  templateUrl: './categoria-productos.component.html',
  styleUrls: ['./categoria-productos.component.scss'],
})
export class CategoriaProductosComponent implements OnInit {
  data$: Observable<CategoriaProducto[]>;
  totalRecords$: Observable<number>;

  constructor (
    public categoriaproductosService: CategoriaProductoService,
    private alert: SweetAlertService
    ) {
    this.data$ = categoriaproductosService.data$;
    this.totalRecords$ = categoriaproductosService.totalRecords$;
  }

  ngOnInit() {
    this.categoriaproductosService.Search();
  }

  async changeStatus(categoria: CategoriaProducto) {
    const change = await this.alert.msgComfirm(
      'Cambiar estado',
      `¿Seguro que quiere ${categoria.activo ? 'inactivar' : 'activar'} la categoria ${categoria.nombreCategoriaProducto}?`);

    if (change) {
      this.categoriaproductosService.changeStatusCategoria(categoria.idCategoriaProducto)
      .subscribe({
        next: (res) => {
          this.alert.msgSimpleSuccess(res.message);
          this.categoriaproductosService.Search();
        },
        error: (e) => this.alert.msgNormalError('Error en el cambio de estado', e.message)
      });
    }
  }
}
