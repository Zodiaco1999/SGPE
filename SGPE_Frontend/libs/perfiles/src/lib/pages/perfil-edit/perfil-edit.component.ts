import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CustomFormBuilderService, SweetAlertService } from '@sgpe-ws/general';
import { Modulo, Perfil, PerfilMenu } from '@sgpe-ws/models'
import { PerfilService } from '@sgpe-ws/services';
import { Observable } from 'rxjs/internal/Observable';
import { ServiceResponse } from '@sgpe-ws/general';
@Component({
  selector: 'sgpe-ws-perfil-edit',
  templateUrl: './perfil-edit.component.html',
  styleUrls: ['./perfil-edit.component.scss'],
})
export class PerfilEditComponent implements OnInit {
  idPerfil: string | null = '';
  menus: PerfilMenu[] = [];
  menusSeleccionados: PerfilMenu[] = [];
  menusTemp: PerfilMenu[] = [];
  modulos: Modulo[] = [];

  constructor(
    private perfilService: PerfilService,
    private route: ActivatedRoute,
    private router: Router,
    private alert: SweetAlertService,
    public formBuilder: CustomFormBuilderService
  ) { }

  async ngOnInit() {
    this.builForm();

    this.idPerfil = this.route.snapshot.paramMap.get('id');
    if (this.idPerfil) {
      this.perfilService.getPerfil(this.idPerfil)
        .subscribe({
          next: (res) => {
            const perfil = res.data;
            this.formBuilder.formGroup.reset(perfil);
            this.menusSeleccionados = perfil.perfilMenus;
            this.getMenus();
          }
        });
    } else {
      this.getMenus();
    }
  }

  getMenus() {
    this.perfilService.getActiveMenus()
      .subscribe({
        next: (res) => {
          const listaMenus = res.data;
          this.modulos = listaMenus.map(menu => {
            return {
              idModulo: menu.idModulo,
              nombreModulo: menu.nombreModulo
            }
          }).filter((mod, i, arr) => arr.findIndex(m => m.idModulo === mod.idModulo) === i);

          if (this.menusSeleccionados.length) {
            for (const ms of this.menusSeleccionados) {
              if (!listaMenus.length) break;
              const menuSeleccionado = listaMenus.find((l) => l.idMenu === ms.idMenu);
              if (menuSeleccionado) {
                const index = listaMenus.indexOf(menuSeleccionado);
                listaMenus.splice(index, 1);
              }
            }
          }
          this.menus = this.menusTemp = listaMenus;
        },
        error: (err) => {
          this.menus = [];
          this.alert.msgNormalError('Error consultando menús', err.messagge);
        }
      });
  }

  savePerfil() {
    if (this.formBuilder.formGroup.valid) {
      const perfil: Perfil = this.formBuilder.formGroup.value;
      perfil.perfilMenus = this.menusSeleccionados;
      if (!this.idPerfil) {
        this.handleCommand(this.perfilService.createPerfil(perfil));
      } else {
        perfil.idPerfil = this.idPerfil;
        this.handleCommand(this.perfilService.updatePerfil(perfil));
      }
    }
  }

  handleCommand(command: Observable<ServiceResponse<object>>) {
    command.subscribe({
      next: (res) => {
        this.router.navigateByUrl('/perfiles');
        this.alert.msgSimpleSuccess(res.message);
      },
      error: (e) => this.alert.msgNormalError(`No se pudo ${this.idPerfil ? 'editar' : 'crear'} el modulo`, e.message)
    })
  }

  seleccionarTodos(selecionado: boolean, menuSeleccionado: PerfilMenu) {
    menuSeleccionado.consulta = menuSeleccionado.menuConsulta
      ? selecionado
      : false;
    menuSeleccionado.inserta = menuSeleccionado.menuInserta
      ? selecionado
      : false;
    menuSeleccionado.actualiza = menuSeleccionado.menuActualiza
      ? selecionado
      : false;
    menuSeleccionado.activa = menuSeleccionado.menuActiva ? selecionado : false;
    menuSeleccionado.elimina = menuSeleccionado.menuElimina
      ? selecionado
      : false;
    menuSeleccionado.ejecuta = menuSeleccionado.menuEjecuta
      ? selecionado
      : false;
    this.validarMenuAsignar(menuSeleccionado);
  }

  seleccionarUno(menuSeleccionado: PerfilMenu) {
    menuSeleccionado.todosInde =
      (menuSeleccionado.consulta ||
        menuSeleccionado.inserta ||
        menuSeleccionado.actualiza ||
        menuSeleccionado.activa ||
        menuSeleccionado.elimina ||
        menuSeleccionado.ejecuta) &&
      !menuSeleccionado.todos;
    this.validarMenuAsignar(menuSeleccionado);
  }

  validarMenuAsignar(menuSeleccionado: PerfilMenu) {
    const menuAgregado = this.menusSeleccionados.find((m) => m.idMenu == menuSeleccionado.idMenu);

    if (!menuAgregado) {
      this.menusSeleccionados.push(menuSeleccionado);
      const index = this.menus.indexOf(menuSeleccionado);
      this.menus.splice(index, 1);
    } else {
      if (
        !menuSeleccionado.consulta &&
        !menuSeleccionado.inserta &&
        !menuSeleccionado.actualiza &&
        !menuSeleccionado.activa &&
        !menuSeleccionado.elimina &&
        !menuSeleccionado.ejecuta
      ) {
        const index = this.menusSeleccionados.indexOf(menuSeleccionado);
        this.menusSeleccionados.splice(index, 1);
        this.getMenus();
      }
    }
  }

  filterByModulos(event: Event) {
    const idModulo = (event.target as HTMLInputElement).value;
    this.menus = idModulo ? this.menusTemp.filter(m => m.idModulo === idModulo) : this.menusTemp;
  }

  builForm() {
    this.formBuilder.formControls = [
      {
        id: 'nombrePerfil',
        name: 'Nombre perfil',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 4
          }
        ]
      },
      {
        id: 'descPerfil',
        name: 'Descripcion perfil',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6
          }
        ]
      }
    ];

    this.formBuilder.initFormGroup();
  }

}
