import { Component, OnInit } from '@angular/core';
import { SweetAlertService } from '@sgpe-ws/general';
import { Usuario } from '@sgpe-ws/models';
import { UsuarioService } from '@sgpe-ws/services'
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-usuarios',
  templateUrl: './usuarios.component.html',
  styleUrls: ['./usuarios.component.scss'],
})
export class UsuariosComponent implements OnInit {
  data$: Observable<Usuario[]>;
  totalRecords$: Observable<number>;

  constructor (
    public usuarioService: UsuarioService,
    private alert: SweetAlertService
  ) {
    this.data$ = usuarioService.data$;
    this.totalRecords$ = usuarioService.totalRecords$;
  }

  ngOnInit(){
    this.usuarioService.Search();
  }

  async changeStatus(usuario: Usuario) {
    const change = await this.alert.msgComfirm('Cambio de estado', `¿Seguro que quiere ${usuario.activo ? 'inactivar' : 'activar'} el usuario ${usuario.nombres} ${usuario.apellidos}?`);

    if (change) {
      this.usuarioService.changeStatusUsuario(usuario.idUsuario)
        .subscribe({
          next: (res) => {
            this.alert.msgSimpleSuccess(res.message);
            this.usuarioService.Search();
          },
          error: (e) => this.alert.msgNormalError('No se pudo cambiar de estado', e.messagge)
        })
    }
  }

}
