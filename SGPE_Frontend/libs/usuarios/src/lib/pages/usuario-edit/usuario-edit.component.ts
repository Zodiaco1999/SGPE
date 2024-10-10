import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomFormBuilderService, SweetAlertService, ServiceResponse } from '@sgpe-ws/general';
import { Empresa, UsuarioEdit, UsuarioPerfil } from '@sgpe-ws/models';
import {
  EmpresaService,
  UsuarioService,
  PerfilService,
} from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-usuario-edit',
  templateUrl: './usuario-edit.component.html',
  styleUrls: ['./usuario-edit.component.scss']
})
export class UsuarioEditComponent implements OnInit {
  idUsuario: string | null = '';
  idPerfil = '';
  empresas: Empresa[] = [];
  perfiles: UsuarioPerfil[] = [];
  perfilesSeleccionados: UsuarioPerfil[] = [];

  constructor(
    private usuarioService: UsuarioService,
    private empresaService: EmpresaService,
    private perfilService: PerfilService,
    private router: Router,
    private route: ActivatedRoute,
    private alert: SweetAlertService,
    public formBuilder: CustomFormBuilderService
  ) {}

  ngOnInit() {
    this.builForm();
    this.idUsuario = this.route.snapshot.paramMap.get('id');

    if (this.idUsuario) {
      this.usuarioService.getUsuario(this.idUsuario).subscribe({
        next: (res) => {
          this.formBuilder.formGroup.reset(res.data);
          this.perfilesSeleccionados = res.data.usuarioPerfils;
        },
        error: (e) => this.alert.msgNormalError('Consulta usuario', e.message),
      });
    }

    this.empresaService.getAllEmpresas().subscribe({
      next: (res) => this.empresas = res.data,
      error: (e) => this.alert.msgNormalError('Consulta empresas', e.message),
    });

    this.perfilService.getActivePerfiles().subscribe({
      next: (res) => this.perfiles = res.data,
      error: (e) => this.alert.msgNormalError('Consulta perfiles', e.message),
    });
  }

  saveUsuario() {
    if (this.formBuilder.formGroup.valid) {
      const valueForm = this.formBuilder.formGroup.value;
      const usuarioForm: UsuarioEdit = {
        ...valueForm,
        cedulaUsuario: valueForm.cedulaUsuario.toString(),
      };

      usuarioForm.usuarioPerfils = this.perfilesSeleccionados;

      if (this.idUsuario) {
        usuarioForm.idUsuario = this.idUsuario;
        this.handleCommand(this.usuarioService.updateUsuario(usuarioForm));
      } else {
        this.handleCommand(this.usuarioService.createUsuario(usuarioForm));
      }
    }
  }

  handleCommand(command: Observable<ServiceResponse<object>>) {
    command.subscribe({
      next: (res) => {
        this.router.navigateByUrl('/usuarios');
        this.alert.msgSimpleSuccess(res.message);
      },
      error: (e) =>
        this.alert.msgNormalError(`No se pudo ${this.idUsuario ? 'editar' : 'crear'} el usuario`,e.message),
    });
  }

  addPerfil() {
    console.log(this.idPerfil)
    const perfilAsignar = this.perfiles.find(p => p.idPerfil === this.idPerfil);
    const perfilExistente = this.perfilesSeleccionados.some(p => p.idPerfil === this.idPerfil);

    if (!perfilExistente && perfilAsignar) {
      this.perfilesSeleccionados.push(perfilAsignar);
    }
  }

  async deletePerfil(perfil: UsuarioPerfil) {
    const remove = await this.alert.msgComfirm('Eliminar perfil', `¿Seguro que quiere eliminar el perfil de ${perfil.nombrePerfil}?`)

    if (remove) {
      const index = this.perfilesSeleccionados.indexOf(perfil);
      this.perfilesSeleccionados.splice(index, 1);
    }
  }

  builForm() {
    this.formBuilder.formControls = [
      {
        id: 'idEmpresa',
        name: 'Empresa',
        formState: 0,
        validatorOrOpts: [
          {
            min: 1,
            validationKey: 'min',
            validationMessage: 'La empresa es requerida',
          },
        ],
      },
      {
        id: 'cedulaUsuario',
        name: 'cedula',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6,
          },
        ],
      },
      {
        id: 'nombres',
        name: 'nombres',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 3,
          },
        ],
      },
      {
        id: 'apellidos',
        name: 'apellidos',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 3,
          },
        ],
      },
      {
        id: 'correo',
        name: 'correo',
        formState: '',
        validatorOrOpts: [
          {
            minLength: 8,
            isEmail: true
          },
        ],
      },
    ];

    this.formBuilder.initFormGroup();
  }
}
