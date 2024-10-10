import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CustomFormBuilderService, SweetAlertService, ServiceResponse } from '@sgpe-ws/general'
import { Menu } from '@sgpe-ws/models';
import { Observable } from 'rxjs/internal/Observable';

@Component({
  selector: 'sgpe-ws-menu-edit',
  templateUrl: './menu-edit.component.html',
  styleUrls: ['./menu-edit.component.scss'],
  providers: [CustomFormBuilderService]
})
export class MenuEditComponent implements OnInit {
  isCreate = false;

  constructor (
    private alert: SweetAlertService,
    public formBuilder: CustomFormBuilderService,
    @Inject(MAT_DIALOG_DATA) public data: Menu,
    private dialogRef: MatDialogRef<MenuEditComponent>
    ) {}

  ngOnInit() {
    this.buildForm();
    this.isCreate = this.data === undefined;
    if (!this.isCreate) {
      this.formBuilder.formGroup.reset(this.data)
    }
    const inOrden = this.formBuilder.formGroup.get('orden');
    const inVisible = this.formBuilder.formGroup.get('visible');
    if (inOrden?.value === 0) {
      inOrden?.disable();
      inVisible?.setValue(false);
    }

    inVisible?.valueChanges.subscribe(
      v => {
        const value = v as boolean;
        if (!value) {
          inOrden?.setValue(0);
          inOrden?.disable();
        } else {
          inOrden?.enable();
        }
      }
    )
  }

  saveMenu() {
    const menu: Menu = this.formBuilder.formGroup.value;
    if (this.isCreate) {
      menu.activo = true;
    }
    menu.orden = menu.visible ? menu.orden : 0;
    this.dialogRef.close(menu);
  }

  handleCommand(command: Observable<ServiceResponse<object>>) {
    command.subscribe({
      next: (res) => {
        this.alert.msgSimpleSuccess(res.message);
      },
      error: (e) => this.alert.msgNormalError('Error en la operación de Menú', e.message)
    })
  }

  buildForm() {
    this.formBuilder.formControls = [
      {
        id: 'idMenu',
        name: 'Id Menu',
        formState: ''
      },
      {
        id: 'nombreMenu',
        name: 'Nombre Menu',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
          },
        ],
      },
      {
        id: 'descMenu',
        name: 'Descripción Menu',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6
          },
        ],
      },
      {
        id: 'etiquetaMenu',
        name: 'Etiqueta Menu',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
          },
        ],
      },
      {
        id: 'url',
        name: 'Url',
        formState: '',
        validatorOrOpts: [
          {
            required: true,
            minLength: 6
          },
        ],
      },
      {
        id: 'visible',
        name: 'Visible',
        formState: false
      },
      {
        id: 'orden',
        name: 'Orden',
        formState: 0,
        validatorOrOpts: [
          {
            required: true,
            min: 0,
            max: 100
          },
        ],
      },
      {
        id: 'consulta',
        name: 'Consulta',
        formState: false,
      },
      {
        id: 'inserta',
        name: 'Inserta',
        formState: false,
      },
      {
        id: 'actualiza',
        name: 'Actualiza',
        formState: false,
      },
      {
        id: 'elimina',
        name: 'Elimina',
        formState: false,
      },
      {
        id: 'activa',
        name: 'Activa',
        formState: false,
      },
      {
        id: 'ejecuta',
        name: 'Ejecuta',
        formState: false,
      },
      {
        id: 'activo',
        name: 'Activo',
        formState: false,
      }
    ];
    this.formBuilder.initFormGroup();
  }
}
