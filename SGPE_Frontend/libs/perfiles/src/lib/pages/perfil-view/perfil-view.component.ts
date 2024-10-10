import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SweetAlertService } from '@sgpe-ws/general';
import { Perfil } from '@sgpe-ws/models';
import { PerfilService } from '@sgpe-ws/services';

@Component({
  selector: 'sgpe-ws-perfil-view',
  templateUrl: './perfil-view.component.html',
  styleUrls: ['./perfil-view.component.scss'],
})
export class PerfilViewComponent implements OnInit {
  perfil: Perfil | null = null;
  idPerfil = '';

  constructor(
    private perfilService: PerfilService,
    private route: ActivatedRoute,
    private alert: SweetAlertService) {}

  ngOnInit() {
    this.idPerfil = this.route.snapshot.paramMap.get('id') ?? '';

    if (this.idPerfil) {
      this.perfilService.getPerfil(this.idPerfil).subscribe({
        next: (res) => this.perfil = res.data,
        error: (err) => this.alert.msgNormalError('Perfil inexistente', err.message)
      })
    }
  }
}
