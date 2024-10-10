import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from '@sgpe-ws/general';
import { Modulo } from '@sgpe-ws/models';
import { ModuloService } from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-modulos',
  templateUrl: './modulos.component.html',
  styleUrls: ['./modulos.component.scss'],
})
export class ModulosComponent implements OnInit {
  data$: Observable<Modulo[]>;
  totalRecords$: Observable<number>;

  constructor(
    public moduloService: ModuloService,
    private alert: SweetAlertService
  ) {
    this.data$ = moduloService.data$;
    this.totalRecords$ = moduloService.totalRecords$;
  }

  ngOnInit() {
    this.moduloService.Search();
  }

  async changeStatusModulo(modulo: Modulo) {
    const change = await this.alert.msgComfirm('Cambiar estado', `¿Seguro que quiere ${modulo.activo ? 'inactivar' : 'activar'} el modulo?`)

    if (change) {
      this.moduloService.changeStatusModulo(modulo.idModulo ?? '')
      .subscribe({
        next: (res) => {
          this.alert.msgSimpleSuccess(res.message);
          this.moduloService.Search();
        },
        error: (e) => this.alert.msgNormalError('Error en el cambio de estado', e.message)
      });
    }
  }
}
