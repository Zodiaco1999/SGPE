import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from '@sgpe-ws/general';
import { Perfil } from '@sgpe-ws/models';
import { PerfilService } from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-perfiles',
  templateUrl: './perfiles.component.html',
  styleUrls: ['./perfiles.component.scss'],
})
export class PerfilesComponent implements OnInit {
  data$: Observable<Perfil[]>;
  totalRecords$: Observable<number>;

  constructor(
    public perfilService: PerfilService,
    private alert: SweetAlertService
  ) {
    this.data$ = perfilService.data$;
    this.totalRecords$ = perfilService.totalRecords$;
  }

  ngOnInit() {
    this.perfilService.Search();
  }

  async changeStatusPerfil(idPerfil: string) {
    const change = await this.alert.msgComfirm('Cambio de estado', '¿Seguro que quiere cambiar el estado?');

    if (change) {
      this.perfilService.changeStatusPerfil(idPerfil)
      .subscribe({
        next: (res) => {
          this.alert.msgSimpleSuccess(res.message);
          this.perfilService.Search();
        },
        error: (e) => this.alert.msgNormalError('Error en el cambio de estado', e.message)
      });
    }
  }
}
