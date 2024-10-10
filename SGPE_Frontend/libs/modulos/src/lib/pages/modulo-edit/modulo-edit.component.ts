import { Component, OnInit } from '@angular/core';
import { MenuService, ModuloService } from '@sgpe-ws/services';
import { CustomFormBuilderService, SweetAlertService, ServiceResponse } from '@sgpe-ws/general'
import { ActivatedRoute, Router } from '@angular/router';
import { Menu, Modulo } from '@sgpe-ws/models';
import { Observable } from 'rxjs/internal/Observable';
import { MenuEditComponent } from '../../components/menu-edit/menu-edit.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'sgpe-ws-modulo-edit',
  templateUrl: './modulo-edit.component.html',
  styleUrls: ['./modulo-edit.component.scss'],
  providers: [CustomFormBuilderService]
})
export class ModuloEditComponent implements OnInit {
  idModulo: string | null = '';
  menus: Menu[] = [];

  constructor(
    private moduloService: ModuloService,
    private menuService: MenuService,
    private alert: SweetAlertService,
    private router: Router,
    private route: ActivatedRoute,
    public formBuilder: CustomFormBuilderService,
    private matDialog: MatDialog
  ) { }

  ngOnInit() {
    this.buildForm();

    this.idModulo = this.route.snapshot.paramMap.get('id');

    if (this.idModulo) {
      this.moduloService.getModulo(this.idModulo)
        .subscribe({
          next: (response) => this.formBuilder.formGroup.reset(response.data),
          error: (e) => this.alert.msgNormalError('Obtener modulo', e.message)
        })

      this.menuService.getMenus(this.idModulo)
      .subscribe({
        next: (response) => this.menus = response.data
      });
    }
  }

  saveModulo() {
    console.log('Entro')
    if (this.formBuilder.formGroup.valid) {
      console.log('Entro valido')
      const modulo: Modulo = this.formBuilder.formGroup.value;
      modulo.menus =  this.menus;
      if (!this.idModulo) {
        this.handleCommand(this.moduloService.createModulo(modulo));
      } else {
        modulo.idModulo = this.idModulo;
        this.handleCommand(this.moduloService.updateModulo(modulo));
      }
    }
  }

  handleCommand(command: Observable<ServiceResponse<object>>) {
    command.subscribe({
      next: (res) => {
        this.router.navigateByUrl('/modulos');
        this.alert.msgSimpleSuccess(res.message);
      },
      error: (e) => this.alert.msgNormalError(`No se pudo ${this.idModulo ? 'editar' : 'crear'} el modulo`, e.message)
    })
  }

  openModal(menu?: Menu) {
    const modalRef = this.matDialog.open(MenuEditComponent,{
      disableClose: true,
      data: menu,
    });

    modalRef
      .afterClosed()
      .subscribe({
        next: (response) => {
          if (response !== '') {
            const currentMenu: Menu = response;
            console.log(response)
            if (!currentMenu.idMenu) {
              currentMenu.idMenu = this.generateGuid();
              this.menus.push(currentMenu)
            } else {
              const menuUpdate = this.menus.find(m => m.idMenu === currentMenu.idMenu);
              if (menuUpdate) {
                const indexMenu = this.menus.indexOf(menuUpdate)
                this.menus[indexMenu] = currentMenu;
              }
            }
            console.log(this.menus)
          }
        },
        error: (err) => console.log(err)
      });
  }

  editMenu(menu: Menu) {
    this.openModal(menu);
  }

  changeStatusMenu(menu: Menu) {
    const indexMenu = this.menus.indexOf(menu);
    const currentMenu = this.menus[indexMenu];
    currentMenu.activo = !currentMenu.activo;
  }

  deleteMenu(menu: Menu) {
    const indexMenu = this.menus.indexOf(menu);
    console.log(indexMenu);
    this.menus.splice(indexMenu, 1);
  }

  buildForm() {
    this.formBuilder.formControls = [
      {
        id: 'nombreModulo',
        name: 'Nombre modulo',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 4
          }
        ]
      },
      {
        id: 'descModulo',
        name: 'Descripcion modulo',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 4
          }
        ]
      },
      {
        id: 'iconoPrefijo',
        name: 'Icono Prefijo',
        formState: '',
        validatorOrOpts: [
          {
            required: true
          }
        ]
      },
      {
        id: 'iconoNombre',
        name: 'Icono Nombre',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 2
          }
        ]
      },
      {
        id: 'orden',
        name: 'Orden visualización',
        formState: 0,
        validatorOrOpts: [
          {
            required: true,
            min: 1
          }
        ]
      },
    ];

    this.formBuilder.initFormGroup();
  }

  generateGuid(): string {
    function s4(): string {
      return Math.floor((1 + Math.random()) * 0x10000)
        .toString(16)
        .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
  }
}
