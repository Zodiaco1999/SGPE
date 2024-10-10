import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SweetAlertService } from '@sgpe-ws/general';
import { UsuarioEdit } from '@sgpe-ws/models';
import { UsuarioService } from '@sgpe-ws/services';

@Component({
  selector: 'sgpe-ws-usuario-view',
  templateUrl: './usuario-view.component.html',
  styleUrls: ['./usuario-view.component.scss'],
})
export class UsuarioViewComponent implements OnInit {
  idUsuario = '';
  usuario: UsuarioEdit | null = null;

  constructor(
    private usuarioService: UsuarioService,
    private route: ActivatedRoute,
    private alert: SweetAlertService) {}

  ngOnInit() {
    this.idUsuario = this.route.snapshot.paramMap.get('id') ?? '';

    this.usuarioService.getUsuario(this.idUsuario).subscribe({
      next: (res) => this.usuario = res.data,
      error: (err) => this.alert.msgNormalError('Usuario inexistente', err.message)
    })
  }
}
